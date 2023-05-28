using Godot;
using System;

public partial class Landing : Node, IState
{
    public IState NextState {get; set;}
    public Entity Character{get;set;}
    public AnimationNodeStateMachinePlayback Playback{get;set;}
    public bool CanMove  {get; set;}

    private Ground _ground;

    public Landing()
    {
        CanMove = false;
    }
    public override void _Ready()
    {
        _ground = GetNode<Ground>("../Ground");  
    }

    public async void Enter()
    {
        {}GD.Print("State Landing");
        await ToSignal(GetTree().CreateTimer(0.05), "timeout");
        NextState = _ground;
    }

    public void Update(double delta)
    {
        Vector2 velocity = Character.Velocity;
        
        velocity.X = Mathf.MoveToward(velocity.X, 0, 800*(float)delta);
        
        Character.Velocity = velocity;
    }
    
    public void Exit()
    {

    }
    public void StateInput(InputEvent @event)
    {
        
    }
}
