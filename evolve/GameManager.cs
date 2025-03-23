using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node2D
{
    // 대화창
 //   //private Control dialogueScene;
 //   public static GameManager Instance { get; private set; }
    
 //   //private Title title;

	////Delegate Delegate { get; set; }
	////DialogueSystem dialogueSystem;

 //   UiManager uiManager;
 //  // FileManager fileManager;

 //   //private PackedScene dialogueScenePacked;

 //  // private Node2D dialogueScene;
 //   // private PackedScene packedScene;
 //   public enum GAMESTATE{
 //       NONE,
 //       LOADING,
 //       GAMESTART,
 //       GAMESTOP
 //   }
 //   private GAMESTATE gameState;

 //   private int Scene_ID = 0;
 //   private int Chat_num = 0;
 //   public enum CHATEVENTSTATE
 //   {
 //       NONE,
 //       TALKING,
 //       ENDED
 //   }

 //   private CHATEVENTSTATE changingState;

 //   public delegate void StateChangeHandler(GAMESTATE newState);
 //   public event StateChangeHandler OnStateChange;

 //   //private Node2D dialogueScene;
 //   //private Label dialogueText;
 //   //private Label nameText;

 //   //private List<Dialogue> DialogueList; // 다이얼로그 리스트

 //   public override void _EnterTree()
 //   {
 //       if(Instance == null)
 //       {
 //           Instance = this;
 //       }
 //       else
 //       {
 //           GD.PrintErr("GameManager is already");
 //           QueueFree();
 //       }
 //   }

 //   public override void _Ready()
	//{
	//	//dialogueSystem = new DialogueSystem();
 // //      AddChild(dialogueSystem);

 //       uiManager = new UiManager();
 //       AddChild(uiManager);

 //       //fileManager = new FileManager();
 //       //AddChild(fileManager);

 //       gameState = GAMESTATE.NONE;
 //       changingState = CHATEVENTSTATE.NONE;
 //       //dialogueScene = GetNode<Node2D>("DialogueScene");

 //       //dialogueScenePacked = GD.Load<PackedScene>("res://DialogueScene.tscn");

 //       //if (dialogueScenePacked != null)
 //       //{
 //       //    dialogueScene = dialogueScenePacked.Instantiate<Node2D>();
 //       //    AddChild(dialogueScene);
 //       //    dialogueScene.Visible = true; // 처음엔 숨김

 //       //    GD.Print("DialogueScene loaded and added to GameManager");
 //       //}
 //       //else
 //       //{
 //       //    GD.PrintErr("Failed to load DialogueScene.tscn");
 //       //}

 //       //dialogueText = (Label)dialogueScene.FindChild("dialogueLabel");
 //       //nameText = (Label)dialogueScene.FindChild("nameLabel");

 //       //DialogueList = new List<Dialogue>();
 //       // dialogueScene = GetNode<Control>("DialogueScene");
 //       //dialogueScene.Visible = false; // 대화창 표시

 //       //title = new Title();
 //       //AddChild(title);

 //       //if (dialogues.Count == 0)
 //       //{
 //       //    GD.Print($"No dialogues found for SceneID {currentSceneID}");
 //       //}
 //       //else
 //       //{
 //       //    foreach (var dialogue in dialogues)
 //       //    {
 //       //        GD.Print($"NPC: {dialogue.NPC}, Dialogue: {dialogue.DialougeText}");
 //       //    }
 //       //}
 //   }
 //   //public override void _Input(InputEvent @event)
 //   //{
 //   //    if (@event is InputEventMouseButton mouseEvent)
 //   //    {
 //   //        if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
 //   //        {
 //   //           if(gameState == GAMESTATE.GAMESTART && changingState == CHATEVENTSTATE.TALKING)
 //   //            {
 //   //                //if(Chat_num >= DialogueList.Count)
 //   //                //{
 //   //                //    changingState = CHATEVENTSTATE.ENDED;
 //   //                //    Chat_num = 0;
 //   //                //    DialogueList.Clear();
 //   //                //    dialogueText.Text = "";
 //   //                //    nameText.Text = "";
 //   //                //    return;
 //   //                //}
 //   //                //dialogueText.Text = DialogueList[Chat_num].DialougeText;
 //   //                //nameText.Text = DialogueList[Chat_num].NPC;
 //   //                //Chat_num++;
 //   //            }
 //   //        }
 //   //    }
 //   //}

 //   //public List<Dialogue> GetDialogue(int currentSceneID)
 //   //{
 //   //    var dialogues = dialogueSystem.GetDialoguesBySceneID(currentSceneID);
 //   //    return dialogues;
 //   //}

 //   //public void SetDialogue(int currentSceneID)
 //   //{
 //   //    //Scene_ID = currentSceneID;
 //   //    //DialogueList = GetDialogue(Scene_ID);
 //   //}

 //   public void SetText(string message)
 //   {
 //     //  dialogueText.Text = message;
 //   }

	//// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
 //   {
 //       switch (gameState) {
 //           case GAMESTATE.NONE:
 //               break;
 //           case GAMESTATE.GAMESTART:
 //               //packedScene.vis
 //               //dialogueScene.Position = new Vector2(100, 100); // 원하는 위치로 설정
 //               //dialogueScene.Scale = new Vector2(1, 1);       // 스케일 확인
 //               //dialogueScene.Visible = true;
 //               //GD.Print("12312312");
 //               break;
 //       }

 //   }
 //   //public void SetActive(PanelContainer node, bool isActive)
 //   //{
 //   //    if(node == null)
 //   //    {
 //   //        GD.Print("null ui");
 //   //    }

 //   //    if (node is PanelContainer canvasItem)
 //   //    {
 //   //        canvasItem.Visible = isActive;
 //   //    }
 //   //    // 3D 노드라면 VisibleInstance 조정

 //   //    // 자식 노드에 재귀적으로 적용
 //   //    //foreach (PanelContainer child in node.GetChildren())
 //   //    //{
 //   //    //    SetActive(child, isActive);
 //   //    //}
 //   //}

 //   //public void ChangeState(GAMESTATE newState)
 //   //{
 //   //    if(gameState != newState)
 //   //    {
 //   //        gameState = newState;

 //   //        OnStateChange?.Invoke(gameState);
 //   //    }
 //   //}

 //   public void SetGameState(string stateName)
 //   {
 //       if (Enum.TryParse(stateName, out GAMESTATE newState))
 //       {
 //           gameState = newState;
 //           GD.Print($"Game state changed to: {gameState}");
 //       }
 //       else
 //       {
 //           GD.PrintErr($"Invalid state name: {stateName}");
 //       }
 //   }


 //   public void SetChatState(string stateName)
 //   {
 //       if (Enum.TryParse(stateName, out CHATEVENTSTATE newState))
 //       {
 //           changingState = newState;
 //           GD.Print($"Game state changed to: {gameState}");
 //       }
 //       else
 //       {
 //           GD.PrintErr($"Invalid state name: {stateName}");
 //       }
 //   }



 //   //public string GetGameState()
 //   //{
 //   //    return gameState.ToString();
 //   //}

 //   public void ChangeScene(string scene)
 //   {
 //       GetTree().ChangeSceneToFile(scene);
 //   }

 //   public void OpenOrCloseUI<T>(T uiElement,bool IsVisible) where T : Control
 //   {
 //       uiManager.OpenOrCloseUI(uiElement,IsVisible);
 //   }

}
