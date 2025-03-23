using Godot;
using System;

public partial class SceneManager : Node
{
    //private static DialogueSystem instance;

    //public static DialogueSystem Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //           // GD.Print("Quest instance null");
    //            //InitalizeDialogue();
    //        }
    //        return instance;
    //    }
    //}
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void ChangeScene(string scenePath)
    {
        if (ResourceLoader.Exists(scenePath))
        {
            GD.Print($"Changing scene to: {scenePath}");
            GetTree().ChangeSceneToFile(scenePath);
        }
        else
        {
            GD.PrintErr($"Scene not found: {scenePath}");
        }
    }
}
