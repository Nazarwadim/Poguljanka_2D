using Godot;
using System;

public partial class Attack : Node, IState
{
    public IState NextState {get; set;}
    public Entity Character{get;set;}
    public AnimationNodeStateMachinePlayback Playback{get;set;}
    public bool CanMove  {get; set;}

    public Attack()
    {
        CanMove = false;
    }

    private Air _air;
    private Attack _attack;
    private Ground _ground;
    public override void _Ready()
    {
        _air = GetNode<Air>("../Air");
        _attack = GetNode<Attack>("../Attack");
        _ground = GetNode<Ground>("../Ground");
        
    }
    public async void Enter()
    {
        Playback.Travel("attack_1");
        {}GD.Print("Attack");
        await ToSignal(GetTree().CreateTimer(0.3), "timeout");
        NextState = _ground;
    }
    
    public void Update(double delta)
    {   
        Vector2 velocity = Character.Velocity;
        
        velocity.X = Mathf.MoveToward(velocity.X, 0, 800*(float)delta);
    
        Character.Velocity = velocity;
    }
    public void Exit(){}
    public void StateInput(InputEvent @event)
    {

    }
}
