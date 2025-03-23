using Godot;
using System;
using System.Collections.Generic;

public abstract partial class GameAnimation : Node
{
    private List<AnimatedSprite2D> animatedSprites;
	protected bool isPlaying = false;
    public void AddSprite(AnimatedSprite2D sprite)
	{
		if (sprite == null)
			throw new ArgumentNullException(nameof(sprite));
		animatedSprites.Add(sprite);
	}
	public void DeleteSprite(AnimatedSprite2D sprite)
	{
        if (sprite == null)
            throw new ArgumentNullException(nameof(sprite));
        animatedSprites.Remove(sprite);
    }
	public void SetSprite(List<AnimatedSprite2D> sprites)
	{
		animatedSprites = sprites ?? throw new ArgumentNullException(nameof(sprites));
	}

	public virtual void StartAnimation()
	{
		foreach(var sprite in animatedSprites)
		{
			sprite.Play();
		}
		isPlaying = true;
	}

	public virtual void StopAnimation()
	{
        foreach (var sprite in animatedSprites)
        {
            sprite.Stop();
        }
        isPlaying = false;
    }

    public abstract void PlayAnimation();
    public abstract void InitializeAnimation();
}

//public partial class AnimationManager : Node
//{
//	// Called when the node enters the scene tree for the first time.
//	private List<AnimatedSprite2D> animatedSprites;
//    public override void _Ready()
//	{
//		animatedSprites = new List<AnimatedSprite2D>();
//	}
	
//	// Called every frame. 'delta' is the elapsed time since the previous frame.
//	public override void _Process(double delta)
//	{

//	}

//	public void AddSprite(AnimatedSprite2D sprite)
//	{
//		animatedSprites.Add(sprite);
//	}

//	public void AddSprite(AnimatedSprite2D[] sprite)
//	{
//		for (int i = 0; i < sprite.Length; i++) {
//			animatedSprites.Add(animatedSprites[i]);
//        }
//	}

//	public void PlayAnimation()
//	{
//        foreach (AnimatedSprite2D sprite in animatedSprites)
//        {
//            //RandomNumberGenerator random = new RandomNumberGenerator();
//            //random.Randomize();

//            //float randomValue = random.RandiRange(0, 3);

//            sprite.Animation = "Idle";
//            sprite.Play();
//        }
//    }
//}
