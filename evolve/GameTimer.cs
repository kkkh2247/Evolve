using Godot;
using System;
using System.Diagnostics;

public partial class GameTimer : Node
{
	private Timer gameTimer;
	private float gameTime;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        SetTimer();
    }

	private void SetTimer()
	{
        gameTimer = new Timer();
        AddChild(gameTimer);
        gameTime = 0f;
        gameTimer.WaitTime = 1.0f;
        gameTimer.OneShot = false;
        gameTimer.Connect("timeout", new Callable(this, nameof(OnTimerTimeout)));
        gameTimer.Start();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        //GD.Print($"Game Time: {gameTime}");
    }

	private void OnTimerTimeout()
	{
		gameTime += 1f;
        var label = GetNode<Label>("gametimetext");
        label.Text = gameTime.ToString();
    }

	
}


