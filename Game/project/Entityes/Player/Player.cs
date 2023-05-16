using Godot;
using System;

public partial class Player : Entity
{
    public static PackedScene PlayerScene = GD.Load<PackedScene>("player.tscn");

    [Export] 
    public float Speed = 300f;
    public float SpeedAcceleration = 3000;
    private StateMashine _mashine;
    private AnimationPlayer _animation;
    public override void _Ready()
    {   
        _animation = GetNode<AnimationPlayer>("AnimationPlayer");
        _mashine = GetNode<StateMashine>("StateMashine");
        GetNode<AnimationPlayer>("AnimationPlayer").Play("Idle");
    }


    public override void _PhysicsProcess(double delta)
    {
       if(_mashine.CurrentState.CanMove){
            MovePlayer(delta);
       }
        _UseGravity(delta);
        _FlipBySpeed();
        MoveAndSlide();
    }
    public override void _Process(double delta)
    {
        GetNode<ProgressBar>("Progress").Value = Health;
    }
    private void MovePlayer(double delta)
    {
        Vector2 velocity = Velocity;
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
            
            if(Mathf.Abs(velocity.X) < Speed )
			    velocity.X += direction.X * SpeedAcceleration * (float)delta;
            
		}
        
		velocity.X = Mathf.MoveToward(velocity.X, 0, 1500 *(float)delta);
        velocity.Y = Mathf.MoveToward(velocity.Y, 0, 300 *(float)delta);
        
        Velocity = velocity;
    }
}