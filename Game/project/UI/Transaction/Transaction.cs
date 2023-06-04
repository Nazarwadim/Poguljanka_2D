using Godot;
using System;

public partial class Transaction : Node
{
    static public Transaction Getsingleton{get; private set;}
    public AnimationPlayer animationPlayer;
    private Node _level;
    public override void _Ready()
    {
        Getsingleton = this;
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }
    public async void LoadGame(string levelPath = " ")
    {
        Transact("res://Game/game.tscn");
        await ToSignal(GetTree().Root, "child_entered_tree");
        _level = GetTree().Root.GetNode("Game/Level");

        if(levelPath != " ")
        {
            LoadLevel(levelPath);
        }
    }
    public void LoadLevel(string path)
    {
        NodeFunctions.DeleteChildrenInNode(_level);
        _level.AddChild( GD.Load<PackedScene>(path).Instantiate() );
    }
    public void LoadMenu()
    {
        Transact("res://UI/MainMenu/menu.tscn");
    }
    public async void Transact(string Target)
    {
        animationPlayer.Play("dissolve");
        await ToSignal(animationPlayer, "animation_finished");
        GetTree().ChangeSceneToFile(Target);
        animationPlayer.PlayBackwards("dissolve");
    }
}
