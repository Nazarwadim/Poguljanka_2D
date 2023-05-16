using Godot;
using System;

public partial class Landing : Node, IState
{
    public IState NextState {get; set;}
    public Entity Character{get;set;}
    public AnimationPlayer Animation {get;set;}
    public bool CanMove  {get; set;}

    private Timer _dalay;
    private Ground _ground;

    public Landing()
    {
        CanMove = false;
    }
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
