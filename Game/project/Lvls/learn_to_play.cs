using Godot;
using System;

public partial class learn_to_play : Node2D
{
	// Called when the node enters the scene tree for the first time.

	public override void _UnhandledInput(InputEvent @event){
		if(@event is InputEventMouseButton eventKey){
			if (eventKey.Pressed)
				{}GD.Print("Clicked");
		}
	}
		public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
