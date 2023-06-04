using Godot;
using System;

public partial class Entity : CharacterBody2D
{
    [Export] public int Health = 10;
    [Export] public int Armor = 0;
    [Export] public Damageable Damage;
    protected readonly float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public void GetDamage(int damage)
    {
        int difference = _CalculateDamage(damage);
        if (difference != 0)
        {
            GetNode<AnimationPlayer>("Damage").Play("damage");
            Health -= difference;
        }
    }
    private int _CalculateDamage(int damage)
    {
        return damage / (int)(1 + 0.1f * Armor);
    }
    protected void _UseGravity(double delta)
    {
        Vector2 velocity = Velocity;
        if (!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
        }
        Velocity = velocity;
    }

    protected void _FlipBySpeed()
    {
        Sprite2D spite = GetNode<Sprite2D>("CharSprite");
        if (Velocity.X > 0)
        {
            spite.FlipH = false;
        }
        if (Velocity.X < 0)
        {
            spite.FlipH = true;
        }
    }
}
