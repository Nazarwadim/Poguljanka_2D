using Godot;
using System;

public partial class Player : Entity
{
    [Export] 
    public float Speed = 300f;

    public override void _Ready()
    {
        GetNode<AnimationPlayer>("AnimationPlayer").Play("Idle");
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }

}