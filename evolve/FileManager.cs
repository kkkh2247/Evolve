using Godot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Security;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

public partial class FileManager : Node
{
    private static FileManager instance;
    public static FileManager Instance
    {
        get
        {
            return instance;
        }
    }

    private Dictionary<Type, Action<object>> dataHandlers;
    private Dictionary<int, QuestData> questDictionary = new Dictionary<int, QuestData>();
    private Dictionary<int, QuestDescriptions> questDescriptDictionary = new Dictionary<int, QuestDescriptions>();
    private Dictionary<int, DialogueData> dialogueDictionary = new Dictionary<int, DialogueData>();

    public override void _Ready()
    {
        // 이미 인스턴스가 존재하면 새로운 인스턴스를 만들지 않음
        if (instance != null)
        {
            QueueFree();  // 중복된 인스턴스는 제거
            return;
        }

        // 인스턴스를 설정
        instance = this;
        GD.Print("퀘스트 싱글톤이 AutoLoad에서 정상적으로 설정됨.");

        InitializeDataHandlers();
    }

    public void InitializeDataHandlers()
    {
        //if (dataHandlers != null && dataHandlers.Count > 0) return; // 이미 초기화된 경우 리턴

        dataHandlers = new Dictionary<Type, Action<object>>
    {
        { typeof(QuestData), (data) =>
            {
                var quest = (QuestData)data;
                questDictionary[quest.Quest_ID] = quest;
            }
        },
        { typeof(QuestDescriptions), (data) =>
            {
                var description = (QuestDescriptions)data;
                questDescriptDictionary[description.Description_ID] = description;
            }
        },
        { typeof(DialogueData),(data) =>
            {
                var dialogue = (DialogueData)data;
                dialogueDictionary[dialogue.ID] = dialogue;
            }

        }
    };
    }
    public IReadOnlyDictionary<int, QuestData> GetQuestDictionary()
    {
        return new ReadOnlyDictionary<int, QuestData>(questDictionary);
    }

    public IReadOnlyDictionary<int, QuestDescriptions> GetQuestDescriptionsDictionary()
    {
        return new ReadOnlyDictionary<int, QuestDescriptions>(questDescriptDictionary);
    }

    public IReadOnlyDictionary<int, DialogueData> GetDialogueData()
    {
        return new ReadOnlyDictionary<int, DialogueData>(dialogueDictionary);
    }


    public string LoadJson(string path)
    {
        if (!Godot.FileAccess.FileExists(path))
        {
            GD.PrintErr($"파일을 찾을 수 없음: {path}");
            return null;
        }

        using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);
        return file.GetAsText();
    }

    public async Task<string> LoadJsonAsync(string path)
    {
        if (!Godot.FileAccess.FileExists(path))
        {
            GD.PrintErr($"파일을 찾을 수 없음: {path}");
            return null;
        }

        // 비동기적으로 파일을 읽어옵니다.
        return await Task.Run(() =>
        {
            using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);
            return file.GetAsText();
        });
    }

    public void SaveJson<T>(string path, T data)
    {
        if (data == null)
        {
            GD.PrintErr("SaveFile: 저장할 데이터가 null입니다.");
            return;
        }

        string json = ConvertJson(data); // 객체를 JSON 문자열로 변환

        using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Write);
        file.StoreString(json);
        GD.Print($"파일 저장 완료: {path}");
    }

    public async Task SaveJsonAsync<T>(string path, T data)
    {
        if (data == null)
        {
            GD.PrintErr("SaveFileAsync: 저장할 데이터가 null입니다.");
            return;
        }

        string json = ConvertJson(data); // 객체를 JSON 문자열로 변환

        // 비동기적으로 파일을 저장
        await Task.Run(() =>
        {
            using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Write);
            file.StoreString(json);
            GD.Print($"파일 저장 완료: {path}");
        });
    }

    public string ConvertJson<T>(T data)
    {
        if (data == null)
        {
            GD.PrintErr("ConvertJson: 데이터가 null입니다.");
            return string.Empty;
        }

        var options = new JsonSerializerOptions { WriteIndented = true }; // JSON을 보기 좋게 정렬
        return JsonSerializer.Serialize(data, options);
    }

    //public async Task<List<T>> LoadCsv<T>(string path) where T : class, new()
    //{
    //    List<T> dataList = new List<T>();

    //    if (!Godot.FileAccess.FileExists(path))
    //    {
    //        GD.PrintErr($"CSV 파일을 찾을 수 없음: {path}");
    //        return dataList;
    //    }

    //    using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);
    //    while (!file.EofReached())
    //    {
    //        string line = file.GetLine();
    //        if (string.IsNullOrWhiteSpace(line))
    //            continue;

    //        string[] fields = line.Split(',');

    //        // T의 타입에 따라 처리 방식 결정
    //        object data = typeof(T) switch
    //        {
    //            Type t when t == typeof(QuestData) => ParseQuestData(fields),
    //            //Type t when t == typeof(ItemData) => ParseItemData(fields),
    //            //Type t when t == typeof(NPCData) => ParseNPCData(fields),
    //            _ => null // _이면 기본값 반환
    //        };

    //        if (data is T typedData)
    //        {
    //            dataList.Add(typedData);
    //        }
    //        else
    //        {
    //            GD.PrintErr($"지원되지 않는 데이터 형식: {typeof(T).Name}");
    //        }
    //    }

    //    return dataList;
    //}

    public async Task<List<T>> LoadCsvAsync<T>(string path) where T : class, new()
    {
        if (!Godot.FileAccess.FileExists(path))
        {
            GD.PrintErr($"CSV 파일을 찾을 수 없음: {path}");
            return new List<T>();
        }

        return await Task.Run(() =>
        {
            List<T> dataList = new List<T>();

            using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);

            // 첫 번째 줄(헤더) 건너뛰기
            file.GetLine();

            while (!file.EofReached())
            {
                string line = file.GetLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                string[] fields = line.Split(',');

                object data = typeof(T) switch
                {
                    Type t when t == typeof(QuestData) => ParseQuestData(fields),
                    Type t when t == typeof(QuestDescriptions) => ParseQuestDescriptionsData(fields),
                    Type t when t == typeof(DialogueData) => ParseDailogData(fields),
                    _ => null
                };

                //if (data is T typedData)
                //{
                //    dataList.Add(typedData);
                //}
                //else
                //{
                //    GD.PrintErr($"지원되지 않는 데이터 형식: {typeof(T).Name}");
                //}

                if (data == null)
                {
                    GD.PrintErr($"데이터 파싱 오류: {typeof(T).Name}");
                    continue;
                }

                // 📌 Dictionary에 등록된 타입인지 확인
                if (dataHandlers.TryGetValue(typeof(T), out var handler))
                {
                    handler(data);
                }
                else
                {
                    GD.PrintErr($"지원되지 않는 데이터 형식: {typeof(T).Name}");
                }
            }

            return dataList;
        });
    }

    private QuestData ParseQuestData(string[] fields)
    {
        if (fields.Length < 7)
        {
            GD.PrintErr("잘못된 퀘스트 CSV 데이터");
            return null;
        }

        return new QuestData
        {
            Quest_ID = int.Parse(fields[0]),
            Quest_Type = fields[1].Trim(),
            Start_NPC = fields[2].Trim(),
            Description_ID = int.Parse(fields[3]),
            Condition = fields[4].Trim(),
            TurnIn_NPC = fields[5].Trim(),
            Rewards = ParseReward(fields[6].Trim())  // ✅ 보상 파싱

        };
    }

    private QuestDescriptions ParseQuestDescriptionsData(string[] fields)
    {
        if(fields.Length < 3)
        {
            GD.PrintErr("잘못된 퀘스트 대화 CSV 데이터");
            return null;
        }

        return new QuestDescriptions {
            Description_ID = int.Parse(fields[0]),
            Questname = fields[1].Trim(),
            Questdescription = fields[2].Trim()    
        };

    }

    private DialogueData ParseDailogData(string[] fields)
    {
        GD.Print("data fields count : " + fields.Length);
        if (fields.Length < 13)
        {
            GD.PrintErr("잘못된 다이얼로그 CSV 데이터: 필드 수 부족");
            return null; // 필드 수 부족하면 null 반환
        }

        return new DialogueData { 
            ID = int.Parse(fields[0]),
            Type = fields[1].Trim(),   
            Character = fields[2].Trim(),
            Portrait = fields[3].Trim(),
            Dialogue = fields[4].Trim(),
            Condition = fields[5].Trim(),  
            NextID = int.Parse(fields[6]),
            NextID_True = int.Parse(fields[7]),
            NextID_False = int.Parse(fields[8]),
            Background = fields[9].Trim(),
            SFX = fields[10].Trim(),
            BGM = fields[11].Trim(),
            EffectScene = fields[12].Trim()
        };

    }

    private List<RewardData> ParseReward(string rewardText)
    {
        List<RewardData> rewards = new List<RewardData>();

        string[] parts = rewardText.Split(' '); // 공백을 기준으로 나눔

        if (parts.Length < 4)
        {
            GD.PrintErr($"보상 형식 오류: {rewardText}");
            return rewards;
        }

        string rewardType = parts[0];  // Gold, XP, Item 등
        string category = parts[1]; // ✅ 무기, 방어구 같은 카테고리
        string rewardValue = parts[2];// 보상 값 (100, Sword 등)
        int amount = parts[3].ToInt(); // ✅ 수량 (기본값 1)

        rewards.Add(new RewardData
        {
            Type = rewardType,
            Category = category,
            Value = rewardValue,
            Amount = amount
        });

        return rewards;
    }


    //private ItemData ParseItemData(string[] fields)
    //{
    //    if (fields.Length < 4)
    //    {
    //        GD.PrintErr("잘못된 아이템 CSV 데이터");
    //        return null;
    //    }

    //    return new ItemData
    //    {
    //        Item_ID = int.Parse(fields[0]),
    //        Item_Name = fields[1].Trim(),
    //        Item_Type = fields[2].Trim(),
    //        Item_Value = int.Parse(fields[3])
    //    };
    //}

    //private NPCData ParseNPCData(string[] fields)
    //{
    //    if (fields.Length < 3)
    //    {
    //        GD.PrintErr("잘못된 NPC CSV 데이터");
    //        return null;
    //    }

    //    return new NPCData
    //    {
    //        NPC_ID = int.Parse(fields[0]),
    //        NPC_Name = fields[1].Trim(),
    //        NPC_Location = fields[2].Trim()
    //    };
    //}
}

/////////////////////////////////////////////////////////////////
/// 플레이어 데이터 가져오기
/// /////////////////////////////////////////////////////////////
//    [Export] private string playerinfo_path = "res://playerinfo.json";
//    private PlayerData playerinfo;
//    private string player_name;
//    public bool Isplayerinfo { get; private set; } = false;
//    /////////////////////////////////////////////////////////////////
//    [Export] private string questinfo_path = "res://CSV/Quests.csv";
//    private QuestData questdata;
//    private Dictionary<int, QuestData> questDictionary = new Dictionary<int, QuestData>();
//    /// <summary>
//    /// ///////////////////////////////////////////////////////////////
//    /// </summary>
//    /// 
//    [Export] private string QuestDescriptions_path = "res://CSV/QuestDescriptions.csv";
//    private QuestDescriptions questDescriptions;
//    private Dictionary<int, QuestDescriptions> questDescriptDictionary= new Dictionary<int, QuestDescriptions>();
//    public override void _Ready()
//    {

//        if(instance == null)
//        {
//            instance = this;
//        }
//        else
//        {
//            QueueFree();
//            return;
//        }

//        playerinfo = new PlayerData();

//        //FileManager.Instance = null;

//        if (IsSaveFileExists(playerinfo_path))
//        {
//            playerinfo = LoadPlayerData();
//            GD.Print(playerinfo.name);
//            Isplayerinfo = true;
//        }
//        else
//        {
//            Isplayerinfo = false;
//            GD.Print("not open playerinfo.json");
//        }

//        if (IsSaveFileExists(questinfo_path))
//        {
//            //questInfo = LoadQuestData();
//            //GD.Print("");
//        }
//        else
//        {
//            GD.Print("not open questinfo.csv");
//        }
//        LoadQuestData();
//        LoadQuestDescriptions();

//        //foreach (RewardData data in questdata.Rewards)
//        //{
//        //    GD.Print(data.Type);
//        //    GD.Print(data.Category);
//        //    GD.Print(data.Value);
//        //    GD.Print(data.Amount);
//        //}
//    }

//    public string LoadJson(string path)
//    {
//        if (!Godot.FileAccess.FileExists(path))
//        {
//            GD.PrintErr($"파일을 찾을 수 없음: {path}");
//            return null;
//        }

//        using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);
//        return file.GetAsText();
//    }

//    public void SaveJson(string path, string data)
//    {
//        using var file = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Write);
//        file.StoreString(data);
//    }


//    private void LoadQuestData()
//    {
//        //if (!File.Exists("Quests.csv"))
//        //    return null;

//        questdata = new QuestData();

//        var file = Godot.FileAccess.Open(questinfo_path, Godot.FileAccess.ModeFlags.Read);
//        file.GetLine(); // 첫 번째 줄 (헤더) 건너뛰기

//        while (!file.EofReached())
//        {
//            string line = file.GetLine();
//            if (string.IsNullOrEmpty(line)) continue;

//            string[] fields = line.Split(',');

//            if (fields.Length < 7)
//            {
//                GD.PrintErr($"잘못된 CSV 형식: {line}");
//                continue;
//            }

//            QuestData quest = new QuestData
//            {
//                Quest_ID = int.Parse(fields[0]),
//                Quest_Type = fields[1].Trim(),
//                Start_NPC = fields[2].Trim(),
//                Description_ID = int.Parse(fields[3]),
//                Condition = fields[4].Trim(),
//                TurnIn_NPC = fields[5].Trim(),
//                Rewards = ParseReward(fields[6].Trim())
//            };
//            questdata = quest;
//            questDictionary[quest.Quest_ID] = quest;
//        }

//        file.Close();
//        GD.Print("퀘스트 데이터 로드 완료!");

//    }

//    private PlayerData LoadPlayerData()
//    {
//        if (!File.Exists("playerinfo.json"))
//            return null;

//        string json = File.ReadAllText("playerinfo.json");
//        return JsonSerializer.Deserialize<PlayerData>(json);
//    }

//    private void LoadQuestDescriptions()
//    {
//        //if (!File.Exists("Quests.csv"))
//        //    return null;

//        var file = Godot.FileAccess.Open(QuestDescriptions_path, Godot.FileAccess.ModeFlags.Read);
//        file.GetLine(); // 첫 번째 줄 (헤더) 건너뛰기

//        while (!file.EofReached())
//        {
//            string line = file.GetLine();
//            if (string.IsNullOrEmpty(line)) continue;

//            string[] fields = line.Split(',');

//            if (fields.Length < 4)
//            {
//                GD.PrintErr($"잘못된 CSV 형식: {line}");
//                continue;
//            }

//            QuestDescriptions questDescriptions = new QuestDescriptions { 
//            Description_ID = int.Parse(fields[0]),
//            Questdescription = fields[1].Trim(),
//            Questname = fields[2].Trim()
//            };
//            questDescriptDictionary[questDescriptions.Description_ID] = questDescriptions;
//        }

//        file.Close();
//        GD.Print("퀘스트 데이터 로드 완료!");

//    }

//    //public string ConvertToJson(PlayerInfo player)
//    //{
//    //    var options = new JsonSerializerOptions { WriteIndented = true};
//    //    return JsonSerializer.Serialize(player,options);
//    //}

//    //public void SaveFile(PlayerInfo playerInfo)
//    //{
//    //    string json = ConvertToJson(playerInfo);
//    //    File.WriteAllText("playerinfo.json",json);
//    //}

//    public string ConvertToJson<T>(T data) where T : class
//    {
//        var options = new JsonSerializerOptions { WriteIndented = true };
//        return JsonSerializer.Serialize(data, options);
//    }

//    public void SaveFile<T>(T file) where T : class
//    {
//        string json = "";
//        string filename = "";
//        switch (file) {
//            case PlayerData:
//                ConvertToJson(playerinfo);
//                filename = "playerinfo.json";
//                break; 
//        }

//        //string json = ConvertToJson(playerInfo);
//        File.WriteAllText(filename, json);
//    }
//    private bool IsSaveFileExists(string path)
//    {
//        if (Godot.FileAccess.FileExists(path))
//        {
//            return true;
//        }
//        else
//        {
//            GD.Print("새로 생성");
//            return false;
//        }
//    }

//    //public void GetName(string name)
//    //{
//    //    player_name = name;
//    //}

//    //private void SetName(string name)
//    //{
//    //    GetName(player_name);
//    //}

//    //private void LoadFile()
//    //{

//    //}

//    //public PlayerInfo GetPlayerInfo()
//    //{
//    //    return playerinfo;
//    //}

//    public string GetPlayerName()
//    {
//        return playerinfo.name;
//    }

//    public bool IsGetplayerinfo()
//    {
//        return Isplayerinfo;
//    }

//    public void DefalutPlayerInfo(string name)
//    {
//        List<string> skill = null;

//        playerinfo = new PlayerData();

//        playerinfo.name = name;
//        playerinfo.hp = -1;
//        playerinfo.mp = -1;
//        playerinfo.experience = -1;
//        playerinfo.level = -1;
//        playerinfo.Skill = skill;
//        // 새로 만들기
//        //ConvertToJson(playerinfo);
//        SaveFile(playerinfo);

//        Isplayerinfo = true;
//    }
//    private List<RewardData> ParseReward(string rewardText)
//    {
//        List<RewardData> rewards = new List<RewardData>();

//        string[] parts = rewardText.Split(' '); // 공백을 기준으로 나눔

//        //if (parts.Length < 2)
//        //{
//        //    GD.PrintErr($"보상 형식 오류: {rewardText}");
//        //    return rewards;
//        //}

//        string rewardType = parts[0];  // Gold, XP, Item 등
//        string category = parts[1]; // ✅ 무기, 방어구 같은 카테고리
//        string rewardValue = parts[2];// 보상 값 (100, Sword 등)
//        int amount = parts[3].ToInt(); // ✅ 수량 (기본값 1)

//        rewards.Add(new RewardData
//        {
//            Type = rewardType,
//            Category = category,
//            Value = rewardValue,
//            Amount = amount
//        });

//        return rewards;
//    }



//}

public class PlayerData
{
    public string name { get; set; }
    public int level { get; set; }
    public int experience { get; set; }
    public int hp { get; set; }
    public int mp { get; set; }
    public List<string> Skill { get; set; }
    public int StoryProgress { get; set; }
}

public class QuestData
{
    public int Quest_ID { get; set; }
    public string Quest_Type { get; set; }
    public string Start_NPC { get; set; }
    public int Description_ID { get; set; }
    public string Condition { get; set; }
    public string TurnIn_NPC { get; set; }
    public List<RewardData> Rewards { get; set; }

    public QuestDescriptions QuestDescriptions { get; set; }
}

public class QuestDescriptions
{
    public int Description_ID { get; set; }
    public string Questname { get; set; }
    public string Questdescription { get; set; }
}

public class RewardData
{
    public string Type { get; set; } // 보상 타입 (Gold, XP, Item 등)
    public string Category { get; set; } // 무기, 방어구 같은 카테고리 (필요한 경우)
    public string Value { get; set; } // 보상 값 (100, Sword 등)
    public int Amount { get; set; } // ✅ 보상 수량 추가
}

public class DialogueData { 
    
    public int ID { get; set; }
    public string Type { get; set; }
    public string Character { get; set; }
    public string Portrait { get; set; }

    public string Dialogue { get; set; }
    public string Condition { get; set; }
    public int NextID { get; set; }
    public int NextID_True { get; set; }
    public int NextID_False { get; set; }
    public string Background { get; set; }
    public string SFX { get; set; }
    public string BGM { get; set; }
    public string EffectScene { get; set; }

}
