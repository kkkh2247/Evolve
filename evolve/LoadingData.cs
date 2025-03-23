using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public partial class LoadingData : Node2D
{
    public async override void _Ready()
    {
        GD.Print("데이터 로딩 시작");

        // 파일 로딩 작업 비동기적으로 실행
        await LoadGameDataAsync();

        // 데이터 로딩 후 Player 싱글톤 초기화
        //if (Player.Instance == null)
        //{
        //    GD.Print("Player 싱글톤 초기화");
        //    Player.InitializePlayer();
        //}
        //else
        //{
        //    GD.Print("Player 데이터 로딩 완료");
        //}

        // Quest 싱글톤 초기화
        //if (Quest.Instance == null)
        //{
        //    GD.Print("Quest 싱글톤 초기화");
        //    Quest.InitalizeQuest();
        //}
        //else
        //{
        //    GD.Print("Quest 데이터 로딩 완료");
        //}

        // DialogueSystem 싱글톤 초기화
        //if (DialogueSystem.Instance == null)
        //{
        //    GD.Print("다이얼로그 싱글톤 초기화");
        //    DialogueSystem.InitalizeDialogue();
        //}
        //else
        //{
        //    GD.Print("다이얼로그 데이터 로딩 완료");
        //}

        // 게임 진행을 위한 로직
        GD.Print("전체 데이터 로딩 완료, 게임 시작");

        // 다음 씬으로 전환
        // GetTree().ChangeScene("res://MainGame.tscn");
        // 또는
        // SceneManager.Instance.ChangeScene("res://Title.tscn");
    }

    private async Task LoadGameDataAsync()
    {
        // 예시: 파일 로딩 비동기 처리
        // 예시로 다이얼로그 데이터 로드
        List<DialogueData> dialogueDatas = await FileManager.Instance.LoadCsvAsync<DialogueData>("res://CSV/Dialogue/Dialogue.csv");

        // 로딩된 다이얼로그 데이터를 DialogueSystem에 세팅
        IReadOnlyDictionary<int, DialogueData> loadedDialogue = FileManager.Instance.GetDialogueData();

        if (loadedDialogue != null && loadedDialogue.Count > 0)
        {
            GD.Print("대화 데이터 로드 완료");

            List<DialogueData> dialogueList = loadedDialogue.Values.ToList();
            DialogueSystem.Instance.SetDialogueData(dialogueList);
        }
        else
        {
            GD.Print("대화 데이터 없음");
        }

        // 여기서 추가적으로 다른 데이터를 비동기적으로 로드할 수 있습니다.
        // 예시: 퀘스트 데이터 로드, 플레이어 데이터 로드 등
        // 이 부분은 추가적인 파일 로딩 코드로 확장 가능합니다.
    }
}
