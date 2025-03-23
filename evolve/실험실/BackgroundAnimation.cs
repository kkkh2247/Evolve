using Godot;
using System;
using System.Collections.Generic;

public partial class BackgroundAnimation : AnimatedSprite2D
{
    [Export] public AnimatedSprite2D animatedSprite;
    [Export] private float animationSpeed;
    public BackgroundAnimation()
    {
        //animatedSprite = null;
    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{

        if(animatedSprite != null)
        {

            animatedSprite.SpeedScale = animationSpeed;
            animatedSprite.Play();
        }
        else
        {
            GD.Print("falsess");
        }
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

    //public override void PlayAnimation()
    //{
    //    if(animatedSprite != null)
    //    {
    //        animatedSprite.Play();
    //    }
    //    throw new NotImplementedException();
    //}

    //public override void InitializeAnimation()
    //{
    //    throw new NotImplementedException();
    //}
}
