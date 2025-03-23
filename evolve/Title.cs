using Godot;
using System;
using System.Collections.Generic;

public partial class Title : Node2D
{
    //[Export] private string TitleMenu_UI_text = "CanvasLayer/TitleMenu"; // 타이틀 메뉴 경로
    [Export] private PanelContainer TitleMenu_UI; // 타이틀 메뉴
    [Export] private PanelContainer LoadCharacterData_UI;// 캐릭터 창
    [Export] private PanelContainer SetCaracterName_UI; // 닉네임 생성 창
    [Export] public float Duration = 1f;  // 애니메이션 지속 시간
    [Export] private string NextScenePath = "res://Laboratory.tscn"; // 저장돼 있는 경로
    [Export] private Button GameStartBtn;
    [Export] private Button SelectSlotBtn;
    [Export] private Button NameCheckBtn;
    [Export] private LineEdit PlayerNameEdit; // 플레이어 입력 받는 텍스트
    [Export] private Label PlayerNameText;
    ///////////////////////////////////////////////////// 

    public enum STATE
    {
        NONE,
        GAMESTART,
        GAMESETTING,
        GAMESTOP
    }

    private STATE state;

    private Camera2D camera; // 카메라
   
    private bool Is_playing = false; 
   
    private bool isTransitioning = false;

    private PanelContainer CharacterSelectUI;

   
	public override void _Ready()
	{

       // TitleMenu_UI = GetNode<PanelContainer>(TitleMenu_UI_text.ToString());
        camera = GetNode<Camera2D>("Camera2D");

        // 비동기 함수 호출 (await으로 기다리기)
        //AnimateButtonOpacity();

        //var dialoguePackedScene = GD.Load<PackedScene>("res://DialogueScene.tscn");

        GameStartBtn.Pressed += GameStartBtn_Pressed;
        SelectSlotBtn.Pressed += SelectSlotBtn_Pressed;
        NameCheckBtn.Pressed += NameCheckBtn_Pressed;
        //var button2 = GetNode<Button>("CanvasLayer/MainMenu/VBoxContainer/Button3");
        //button2.Pressed += Button_Pressed2;

        state = STATE.NONE;
      //  GD.Print("현재 플레이어 정보가 있는가?1 " + FileManager.Instance.IsGetplayerinfo());
    }

    public override void _Process(double delta)
    {
       //if(state == STATE.GAMESTART)
       // {
       //     dialogueScene.Visible = true;
       // }
    }

    void GameStartBtn_Pressed() // 게임 시작 버튼
    {
      
        //ChangeScene();
        GD.Print("test");
       
        ShowSelectSlot();
       
    }
    void SelectSlotBtn_Pressed() // 캐릭터 선택 버튼
    {
        //GD.Print("현재 플레이어 정보가 있는가? " + FileManager.Instance.IsGetplayerinfo());
        //if (FileManager.Instance.IsGetplayerinfo()) // 저장된게 있다면
        //{
        //    ChangeScene();
        //    GameManager.Instance.SetGameState("GAMESTART");
        //}
        //else
        //{
        //    GameManager.Instance.OpenOrCloseUI<PanelContainer>(SetCaracterName_UI, true);
        //    GameManager.Instance.OpenOrCloseUI<PanelContainer>(LoadCharacterData_UI, false);
        //    GameManager.Instance.OpenOrCloseUI<PanelContainer>(TitleMenu_UI, false);
        //}
        //// 여기서 LoadPlayerData , playerdata가 있는지 확인 (없으면 새로 생성)
        ChangeScene();
        
    }

    void NameCheckBtn_Pressed() // 닉네임 확인 버튼
    {
     //   FileManager.Instance.DefalutPlayerInfo(PlayerNameEdit.Text); // 플레이어 정보 생성

        ChangeScene();
       // GameManager.Instance.SetGameState("GAMESTART");

        //GameManager.Instance.OpenOrCloseUI<PanelContainer>(SetCaracterName_UI, false);
        //GameManager.Instance.OpenOrCloseUI<PanelContainer>(LoadCharacterData_UI, true);
        //GameManager.Instance.OpenOrCloseUI<PanelContainer>(TitleMenu_UI, true);
    }

    private void ShowSelectSlot()
    {
        // GameManager.Instance.SetActive(LoadCharacterData_UI, true);
       // GameManager.Instance.OpenOrCloseUI<PanelContainer>(LoadCharacterData_UI,true);
       // GameManager.Instance.OpenOrCloseUI<PanelContainer>(TitleMenu_UI, false);



        //if (FileManager.Instance.IsGetplayerinfo()) // 저장된게 있다면
        //{
        //    GD.Print("get player name");
        //    PlayerNameText.Text = FileManager.Instance.GetPlayerName();
        //}
        //else
        //{
        //    GD.Print("not get player name");

        //}
        ////GameManager.Instance.SetActive(TitleMenu_UI, false);

    }

    //void Button_Pressed2()
    //{
    //    GD.Print("test2");   
    //}

    private async void ChangeScene()
    {
        isTransitioning = true; // 씬 전환 상태 설정
        GD.Print("Changing scene...");

        // 씬 전환 효과 (예: 페이드 아웃)
        const float fadeDuration = 0.5f;
        const int steps = 30;
        var stepDuration = fadeDuration / steps;

        var canvas = GetNode<CanvasLayer>("CanvasLayer");
        var colorRect = new ColorRect { Color = new Color(0, 0, 0, 0), Name = "FadeEffect" };
        canvas.AddChild(colorRect);
        colorRect.SetSize(GetViewportRect().Size);

        // 페이드 아웃 애니메이션
        for (int i = 0; i <= steps; i++)
        {
            var t = (float)i / steps;
            colorRect.Color = new Color(0, 0, 0, t);
            await ToSignal(GetTree().CreateTimer(stepDuration), "timeout");
        }

        // 씬 이동
       // GameManager.Instance.ChangeScene(NextScenePath);
        //GetTree().ChangeSceneToFile(NextScenePath);

        //// 페이드 인 애니메이션
        //for (int i = steps; i >= 0; i--)
        //{
        //    var t = (float)i / steps;
        //    colorRect.Color = new Color(0, 0, 0, t);
        //    await ToSignal(GetTree().CreateTimer(stepDuration), "timeout");
        //}

        //// 페이드 효과 제거
        //colorRect.QueueFree();

        isTransitioning = false; // 씬 전환 상태 해제
    }

    //public override void _Input(InputEvent @event)
    //{
    //    if(@event is InputEventMouseButton mouseEvent)
    //    {
    //        if(mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
    //        {
    //            if (Is_playing)
    //            {
    //                return;
    //            }
    //            ShowMenu();
    //            Is_playing = true;
    //        }
    //    }
    //}

    // 비동기 함수 정의
    private  void AnimateButtonOpacity()
{
	//var button = GetNode<Button>("Button");
    

	//const int steps = 20; // 애니메이션 단계 수
	//var stepDuration = Duration / steps; // 각 단계의 지속 시간

	//while (true)
	//{
		
	//	// 1. 투명도를 점진적으로 감소 (100% -> 0%)
	//	for (int i = 0; i <= steps; i++)
	//	{
	//		var t = (float)i / steps;
	//		var opacity = Mathf.Lerp(1.0f, 0.0f, t); // 선형 보간으로 투명도 계산
	//		button.Modulate = new Color(1.0f, 1.0f, 1.0f, opacity);
	//		await ToSignal(GetTree().CreateTimer(stepDuration), "timeout");
	//	}

	//	// 2. 투명도를 점진적으로 증가 (0% -> 100%)
	//	for (int i = 0; i <= steps; i++)
	//	{
	//		var t = (float)i / steps;
	//		var opacity = Mathf.Lerp(0.0f, 1.0f, t); // 선형 보간으로 투명도 계산
	//		button.Modulate = new Color(1.0f, 1.0f, 1.0f, opacity);
	//		await ToSignal(GetTree().CreateTimer(stepDuration), "timeout");
	//	}
	//}
}

    private void ShowMenu()
    {
        //GameManager.Instance.SetActive(TitleMenu_UI,true);
		//GD.Print("btn");

  //      const float zoomDuration = 1.0f;
  //      const int steps = 60;
  //      var stepDuration = zoomDuration / steps;

  //      Vector2 targetPos = new Vector2(340, 150);
  //      Vector2 initialPos = container.Position;
  //      const float moveDuration = 1.0f;
  //      stepDuration = moveDuration / steps;

  //      for (int i = 0; i <= steps; i++)
  //      {
  //          var t = (float)i / steps;
  //          container.Position = initialPos.Lerp(targetPos, t); // 선형 보간
  //          await ToSignal(GetTree().CreateTimer(stepDuration), "timeout");
  //      }



        //Vector2 targetZoom = new Vector2(2.8f,3.8f);
        //Vector2 initalZoom = camera.Zoom;
      
      

        //for (int i = 0; i <= steps;i++)
        //{
        //	var t = (float)i / steps;
        //	camera.Zoom = initalZoom.Lerp(targetZoom,t);
        //	await ToSignal(GetTree().CreateTimer(stepDuration), "timeout");
        //}


    }
}
