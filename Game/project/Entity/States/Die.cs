using Godot;
using System;

public partial class Die : Node, IState
{
    public IState NextState { get; set; }
    public Entity Character { get; set; }
    public AnimationNodeStateMachinePlayback Playback { get; set; }
    public AnimationTree AnimTree{get;set;}
    public bool CanMove { get; set; }
    public Die()
    {
        CanMove = false;
    }
    public void Enter()
    {
        if (Character.Health <= 0)
        {
            PlayDie();
        }
        else
        {
            throw new Exception("Exception in Die.cs line 17. Die state can call only if Health <= 0");
        }
    }

    private async void PlayDie()
    {
        Playback.Travel("die");

        await ToSignal(AnimTree, "animation_finished");
        Character.QueueFree();
    }
    public void Update(double delta)
    {
        Vector2 velocity = Character.Velocity;
        velocity.X = Mathf.MoveToward(velocity.X, 0, 500 * (float)delta);
        Character.Velocity = velocity;
    }

    public void StateInput(InputEvent @event)
    { }
    public void Exit()
    {

    }
}