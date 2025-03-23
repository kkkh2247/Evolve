using Godot;
using System;
using System.Collections.Generic;

public partial class Tutorial : Node2D
{

    // 1 = 튜토리얼
	// Called when the node enters the scene tree for the first time.
    //private List<>
	public override void _Ready()
	{
        //GameManager.Instance.SetChatState("TALKING");
        //DialogueSystem.Instance.test();
        //List<Dialogue> list = new List<Dialogue>();
        //list = DialogueSystem.Instance.GetDialoguesByNPC("닥터");

        //foreach (Dialogue text in list)
        //{
        //    GD.Print(text.DialougeText);
        //}
        // GameManager.Instance.SetDialogue(2);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
