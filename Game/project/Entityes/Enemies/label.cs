using Godot;
using System;

public partial class label : Label
{
	[Export] Vector2 float_speed = new Vector2(0,-50);
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += float_speed * (float)delta;
	}

	public void _on_timer_timeout(){
		QueueFree();
	}
}
