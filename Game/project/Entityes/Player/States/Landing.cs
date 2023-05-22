using Godot;
using System;

public partial class Landing : Node, IState
{
    public IState NextState {get; set;}
    public Entity Character{get;set;}
    public AnimationPlayer Animation {get;set;}
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
        Animation.Play("land");
        await ToSignal(Animation, "animation_finished");
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
        
    }
}
