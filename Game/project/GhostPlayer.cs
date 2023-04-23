using Godot;
using System;

public partial class GhostPlayer : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Sprite2D>("Ghost2D");
		GetNode<AnimationPlayer>("AnimationPlayer").Play("FadeOut");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void SetHValue(bool value){
		GetNode<Sprite2D>("Ghost2D").FlipH = value;
	}
}
