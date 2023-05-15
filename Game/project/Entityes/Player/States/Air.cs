using Godot;
using System;

public partial class Air : Node, IState
{
    public IState NextState {get; set;}
    public CharacterBody2D Character{get;set;}
    public AnimationPlayer Animation {get;set;}
    public StateMashine Mashine {get;set;}
    

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
        GD.Print("Air");
        Mashine.CanMove = false;
    }
    
    public void Update(double delta)
    {
        if(Character.IsOnFloor() == true)
        {
            // in this method i might add that it emit signal fall on the land and get damage....///
            NextState = _land;
        }
    }
    public void Exit()
    {

    }
    public void StateInput(InputEvent @event)
    {

    }
}
