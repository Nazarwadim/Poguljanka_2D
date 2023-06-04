using Godot;
using System;

public partial class Air : Node, IState
{
    public IState NextState {get; set;}
    public Entity Character{get;set;}
    public AnimationNodeStateMachinePlayback Playback{get;set;}
    public AnimationTree AnimTree{get;set;}
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
        Playback.Travel("jump_start");
        {}GD.Print("Air");
    }

    private float _prevYSpeed;    
    private const float MIN_Y_SPEED_FOR_DAMAGE = 500;
    public void Update(double delta)
    {
        if(Character.IsOnFloor() == true)
        {
            if(_prevYSpeed > MIN_Y_SPEED_FOR_DAMAGE){
                {}GD.Print( "Damage is " +(int)(_prevYSpeed * 0.02f));
                Character.GetDamage((int)(_prevYSpeed * 0.02f));
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
