using Godot;
using System;

public partial class Transaction : Node
{
    public AnimationPlayer animationPlayer;
    public override void _Ready()
    {
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
