using Godot;
using System;

public partial class Entity : CharacterBody2D
{
	protected readonly float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	
	protected void _UseGravity(double delta)
	{
		Vector2 velocity = Velocity;
		if(!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}
		Velocity = velocity;
	}
	protected void _FlipBySpeed()
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
}
