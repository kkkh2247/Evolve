using Godot;
using System;
using System.Collections.Generic;

public partial class Tadpole : Node
{
    // Called when the node enters the scene tree for the first time.
    private List<AnimatedSprite2D> animatedSprites = new List<AnimatedSprite2D>();
    public override void _Ready()
	{
        animatedSprites.Add(GetNode<AnimatedSprite2D>("tadpole0"));
        animatedSprites.Add(GetNode<AnimatedSprite2D>("tadpole1"));
        animatedSprites.Add(GetNode<AnimatedSprite2D>("tadpole2"));
        
        foreach (AnimatedSprite2D tadpole in animatedSprites)
        {
            RandomNumberGenerator random = new RandomNumberGenerator();
            random.Randomize();

            float randomValue = random.RandiRange(0, 3);

            tadpole.Animation = "Idle";
            tadpole.Play();
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
