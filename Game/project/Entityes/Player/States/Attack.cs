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
        CanMove = true;
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
        {}GD.Print("Attack");
        Animation.Play("attack_1");
        await ToSignal(Animation, "animation_finished");
        NextState = _ground;
    }
    
    public void Update(double delta)
    {   
    }
    public void Exit(){}
    public void StateInput(InputEvent @event)
    {

    }
}
