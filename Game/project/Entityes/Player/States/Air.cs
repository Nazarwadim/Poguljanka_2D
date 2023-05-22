using Godot;
using System;

public partial class Air : Node, IState
{
    public IState NextState {get; set;}
    public Entity Character{get;set;}
    public AnimationPlayer Animation {get;set;}
    public bool CanMove  {get; set;}
    public Air()
    {
        CanMove = false;
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
        Animation.Play("jump_start");
        {}GD.Print("Air");
    }

    private float _prevYSpeed;    
    private const float MIN_Y_SPEED_FOR_DAMAGE = 450;
    public void Update(double delta)
    {
        if(Character.IsOnFloor() == true)
        {
            if(_prevYSpeed > MIN_Y_SPEED_FOR_DAMAGE){
                {}GD.Print( "Damage is " +(int)(_prevYSpeed * 0.01f));
                Character.GetDamage((int)(_prevYSpeed * 0.01f));
            }
            NextState = _land;
        }
        _prevYSpeed = Character.Velocity.Y;
    }
    public void Exit()
    {

    }
    public void StateInput(InputEvent @event)
    {

    }
}
