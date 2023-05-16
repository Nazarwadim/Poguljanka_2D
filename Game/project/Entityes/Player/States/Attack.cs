using Godot;
using System;

public partial class Attack : Node, IState
{
    public IState NextState {get; set;}
    public Entity Character{get;set;}
    public AnimationPlayer Animation {get;set;}
    public bool CanMove  {get; set;}

    public Attack()
    {
        CanMove = false;
    }

    private Timer _dalay;
    private Air _air;
    private Attack _attack;
    private Ground _ground;
    public override void _Ready()
    {
        
        _air = GetNode<Air>("../Air");
        _dalay = GetNode<Timer>("Dalay");
        _attack = GetNode<Attack>("../Attack");
        _ground = GetNode<Ground>("../Ground");

        _dalay.Timeout +=Exit;
    }
    public void Enter()
    {
        GD.Print("Attack");
        _dalay.Start();
        Animation.Play("attack_1");
    }
    
    public void Update(double delta)
    {
        
    }
    public void Exit()
    {
        NextState = _ground;
    }
    public void StateInput(InputEvent @event)
    {

    }
}
