using Godot;
using System;

public partial class BarSprite : Sprite2D
{
    protected readonly float Velocity = ProjectSettings.GetSetting("physics/2d/default_velocity").AsSingle();
    [Export]
    public float Speed = 350f;
    public float SpeedAcceleration = 4000;
    [Export] Vector2 direction = Vector2.Right;
    public AnimationPlayer Animation { get; set; }

    private StateMashine _mashine;
    private AnimationPlayer _animation;
    public override void _Ready()
    {
        GetNode<AnimationPlayer>("AnimationLoad").Play("BarAnimation");
    }

    public override void _PhysicsProcess(double delta)
    {
       
    }
}
