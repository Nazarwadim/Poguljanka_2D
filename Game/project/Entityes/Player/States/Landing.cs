using Godot;
using System;

public partial class Landing : Node, IState
{
    public IState NextState {get; set;}
    public CharacterBody2D Character{get;set;}
    public AnimationPlayer Animation {get;set;}
    public StateMashine Mashine {get;set;}

    private Timer _dalay;
    private Ground _ground;
    public override void _Ready()
    {
        _dalay = GetNode<Timer>("Dalay");
        _ground = GetNode<Ground>("../Ground");
        _dalay.Timeout += TimerTimeout;
    }

    public void Enter()
    {
        GD.Print("State Landing");
        Animation.Play("land");
        Mashine.CanMove = false;
        _dalay.Start();
    }

    public void TimerTimeout()
    {
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
