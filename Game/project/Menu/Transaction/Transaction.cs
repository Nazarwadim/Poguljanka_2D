using Godot;
using System;

public partial class Transaction : Node
{
    static public Transaction Getsingleton{get; private set;}
    public AnimationPlayer animationPlayer;
    public override void _Ready()
    {
        Getsingleton = this;
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public async void Transact(string Target)
    {
        animationPlayer.Play("dissolve");
        await ToSignal(animationPlayer, "animation_finished");
        GetTree().ChangeSceneToFile(Target);
        animationPlayer.PlayBackwards("dissolve");
    }
}
