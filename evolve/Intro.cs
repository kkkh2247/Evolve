using Godot;
using System;

public partial class Intro : Node2D
{
    private string NextScenePath = "Loading.tscn";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        ChangeScene();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    private async void ChangeScene()
    {
        //isTransitioning = true; // 씬 전환 상태 설정
        GD.Print("Changing scene...");

        // 씬 전환 효과 (예: 페이드 아웃)
        const float fadeDuration = 0.5f;
        const int steps = 30;
        var stepDuration = fadeDuration / steps;

        var canvas = GetNode<CanvasLayer>("CanvasLayer");
        var colorRect = new ColorRect { Color = new Color(0, 0, 0, 0), Name = "FadeEffect" };
        canvas.AddChild(colorRect);
        colorRect.SetSize(GetViewportRect().Size);

        //페이드 인 애니메이션
        for (int i = steps; i >= 0; i--)
        {
            var t = (float)i / steps;
            colorRect.Color = new Color(0, 0, 0, t);
            await ToSignal(GetTree().CreateTimer(stepDuration), "timeout");
        }

        const float waitDuration = 1.0f; // 대기 시간 (초)
        await ToSignal(GetTree().CreateTimer(waitDuration), "timeout");

        //페이드 아웃 애니메이션
        for (int i = 0; i <= steps; i++)
        {
            var t = (float)i / steps;
            colorRect.Color = new Color(0, 0, 0, t);
            await ToSignal(GetTree().CreateTimer(stepDuration), "timeout");
        }

        //씬 이동
        GetTree().ChangeSceneToFile(NextScenePath);

        // 페이드 효과 제거
        //colorRect.QueueFree();

        //isTransitioning = false; // 씬 전환 상태 해제
    }
}
