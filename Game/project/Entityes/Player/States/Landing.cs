using Godot;
using System;

public partial class Landing : Node, IState
{
    public IState NextState {get; set;}
    public Entity Character{get;set;}
    public AnimationPlayer Animation {get;set;}
    public bool CanMove  {get; set;}
    public bool CanDash  {get; set;}
    [Export] public float DashVelosity = 1000f;
    [Export] public Vector2 direction;

    public int lastDirection { get; set; }

    private Ground _ground;

    public Landing()
    {
        CanMove = true;
        CanDash = true;
    }
    public override void _Ready()
    {
        _ground = GetNode<Ground>("../Ground");  
    }

    public void Enter()
    {
        {}GD.Print("State Landing");
        Animation.Play("land");
        NextState = _ground;
    }

    public void Update(double delta)
    {
         
    }
    
    public void Exit()
    {

    }
    public void StateInput(InputEvent @event)
    {
        if (direction.X < 0) 
        lastDirection = -1;
        else if (direction.X > 0)
           lastDirection = 1;

        if(@event.IsActionPressed("dash"))
        {
            _Dash(lastDirection);
        }
        direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
    }
    private void _Dash(int i)
    {
        Vector2 velocity = Character.Velocity;
        velocity.X = i*DashVelosity;
        Character.Velocity = velocity;
    }
}
