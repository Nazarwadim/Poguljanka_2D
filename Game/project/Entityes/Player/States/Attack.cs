using Godot;
using System;

public partial class Attack : Node, IState
{
    public IState NextState {get; set;}
    public CharacterBody2D Character{get;set;}
    public AnimationPlayer Animation {get;set;}

    private StateMashine _mashine;
    
    public void Enter()
    {
        _mashine.CanMove = true;
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
