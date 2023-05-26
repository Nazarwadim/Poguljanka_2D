using Godot;
using System;

public partial class Air : Node, IState
{
    public IState NextState {get; set;}
    public Entity Character{get;set;}
    public AnimationPlayer Animation {get;set;}
    public bool CanMove  {get; set;}
    [Export] public float JumpVelosity = -200f;
    [Export] public float DashVelosity = 1000f;
    [Export] public Vector2 direction;

    public static int lastDirection { get; set; }
    public bool CanJump { get; set; }
    public bool CanDash  {get; set;}
    public Air()
    {
        CanMove = true;
        CanJump = true;
        CanDash = true;
    }
    private Attack _attack;
    private Landing _land;
    private Ground _ground;
    public override void _Ready()
    {
        _ground = GetNode<Ground>("../Ground");
        _attack = GetNode<Attack>("../Attack");
        _land = GetNode<Landing>("../Landing");
    }
    public void Enter()
    {
        { }GD.Print("Air");
        Animation.Play("jump_start");
    }

    private float _prevYSpeed;    
    private const float MIN_Y_SPEED_FOR_DAMAGE = 1200;
    public void Update(double delta)
    {
        if(Character.IsOnFloor() == true)
        {
            if(_prevYSpeed > MIN_Y_SPEED_FOR_DAMAGE){
                {}GD.Print( "Damage is " +(int)(_prevYSpeed * 0.02f));
                Character.GetDamage((int)(_prevYSpeed * 0.02f));
            }
            NextState = _land;
            CanJump = true;
        }
        _prevYSpeed = Character.Velocity.Y;
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

        if(CanJump != false)
        if (@event.IsActionPressed("jump"))
        {
            _Jump();
            CanJump = false;
        }
        if (@event.IsActionPressed("attack"))
        {
            _Attack();
        }
        if(@event.IsActionPressed("dash"))
        {
            _Dash(lastDirection);
        }
        direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Ground ground = new Ground();
        Ground.lastDirection = lastDirection; 
    }
    private void _Jump()
    {
        Vector2 velocity = Character.Velocity;
        velocity.Y = JumpVelosity - 150f;
        Character.Velocity = velocity;
    }

    private void _Attack()
    {
        NextState = _attack;
    }
    private void _Dash(int i)
    {
        Vector2 velocity = Character.Velocity;
        velocity.X = i*DashVelosity;
        Character.Velocity = velocity;
    }
}
