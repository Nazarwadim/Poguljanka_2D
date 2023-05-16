using Godot;
using System;

public partial class Entity : CharacterBody2D
{
	[Export] public int Health = 10;
	[Export] public int Armor = 0;

	public void GetDamage(int damage)
	{
		Health -= _CalculateDamage(damage);
	}
	private int _CalculateDamage(int damage)
	{
		return damage / (int)(1 + 0.1f* Armor); 
	}
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
		Sprite2D spite = GetChild<Sprite2D>(0);
		if(Velocity.X > 0)
		{
			spite.FlipH = false;
		}
		if(Velocity.X < 0)
		{
			spite.FlipH = true;
		}
	}
}
