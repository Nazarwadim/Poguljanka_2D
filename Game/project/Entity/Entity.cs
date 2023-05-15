using Godot;
using System;

public partial class Entity : CharacterBody2D
{
	private void FlipBySpeed()
	{
		
		if(Velocity.X > 0)
		{
		 GetChild<Sprite2D>(0).FlipH = false;
		}
		if(Velocity.X < 0)
		{
			GetChild<Sprite2D>(0).FlipH = true;
		}
	}
    public override void _Process(double delta)
    {
        FlipBySpeed();
    }
}
