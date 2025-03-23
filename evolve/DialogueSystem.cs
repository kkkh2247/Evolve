using Godot;
using System;
using System.Collections.Generic;

public partial class DialogueSystem : Node
{
    private static DialogueSystem instance;
    public static DialogueSystem Instance
    {
        get
        {
            return instance;
        }
    }

    public override void _Ready()
    {
        if (instance == null)
        {
            instance = this; // 🚀 AutoLoad 싱글톤을 그대로 사용!
            GD.Print("다이얼로그 싱글톤이 AutoLoad에서 정상적으로 설정됨.");
        }
        else
        {
            GD.PrintErr("이미 존재하는 다이얼로그 싱글톤이 있음!");
        }
    }

    //public static void InitalizeDialogue()
    //{
    //    if (instance != null)
    //    {
    //        GD.PrintErr("다이얼로그 인스턴스가 이미 초기화되었습니다.");
    //        return;
    //    }

    //    // AutoLoad에서 등록된 DialogueSystem을 사용
    //    instance = DialogueSystem.Instance;  // AutoLoad된 인스턴스를 가져옴

    //    if (instance == null)
    //    {
    //        GD.PrintErr("AutoLoad에서 DialogueSystem을 찾을 수 없습니다.");
    //        return;
    //    }

    //    GD.Print("DialogueSystem 싱글톤이 AutoLoad에서 정상적으로 설정됨");
    //}

    private Dictionary<int, DialogueData> DialogueDictionary = new Dictionary<int, DialogueData>();
    private int currentDialogueID = 0; // 현재 대화의 ID

    private enum STATE
    {
        None = 0,
        Chatting = 1,
        END
    }

    private STATE state;

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventKey keyEvent && keyEvent.Pressed) // keyEvent.Pressed는 키가 눌렸을 때만 처리
        {
            // 'q' 키가 눌렸을 때
            if (keyEvent.Keycode == Key.Q)
            {
                if (DialogueDictionary.Count > 0) // 빈 딕셔너리가 아닌지 확인
                {
                    GD.Print("????????");
                }
                else
                {
                    GD.Print("!!!!!!!!!");
                }

                state = STATE.Chatting;
                if (state == STATE.Chatting)
                {
                    StartDialogue(0);
                    StartDialogue(1);
                }
                state = STATE.END;
            }
        }
    }

    public void SetDialogueData(List<DialogueData> dialogueData)
    {
        if (dialogueData == null || dialogueData.Count == 0)
        {
            GD.PrintErr("대화 데이터가 비어있거나 null 입니다.");
            return;
        }

        DialogueDictionary.Clear();

        // 데이터를 DialogueDictionary에 추가하고 출력
        foreach (DialogueData data in dialogueData)
        {
            DialogueDictionary[data.ID] = data;
            GD.Print($"대화 ID: {data.ID}, 내용: {data.Dialogue}, {data.Type}, {data.SFX}, {data.NextID}");
        }

        GD.Print($"대화 데이터가 {DialogueDictionary.Count} 개 로드되었습니다.");
    }

    public void StartDialogue(int startingID)
    {
        if (!DialogueDictionary.ContainsKey(startingID))
        {
            GD.PrintErr("시작할 대화 ID가 존재하지 않습니다.");
            return;
        }
        currentDialogueID = startingID;
    }
}
