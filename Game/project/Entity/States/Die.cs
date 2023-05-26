using Godot;
using System;

public partial class Die : Node, IState
{
    public IState NextState {get;set;}
    public Entity Character {get;set;}
    public AnimationPlayer Animation {get;set;}
    public bool CanMove  {get; set;}
    public void Enter()
    {
        if(Character.Health <= 0){
            PlayDie();
        }
        else{
            throw new Exception("Exception in Die.cs line 17. Die state can call only if Health <= 0");
        }
    }

    private async void PlayDie()
    {
        Animation.Play("die");

        await ToSignal(Animation, "animation_finished");
        var transaction = GetNode<Transaction>("/root/Transaction");
		transaction.transact("res://Menu/menu.tscn");
        Character.QueueFree();
    }
    public void Update(double delta)
    {
        Vector2 velocity = Character.Velocity;
        velocity.X = Mathf.MoveToward(velocity.X, 0, 500*(float)delta);
        Character.Velocity = velocity;
    }
    
    public void StateInput(InputEvent @event)
    {}
    public void Exit()
    {
        
    }
}