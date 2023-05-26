using Godot;
using System;

public partial class Sword : Area2D
{
	
	// Called when the node enters the scene tree for the first time.
    [Export] public int damage = 10;
	private void _on_body_entered(Node body){
		if (body is Damagable damagable)
        {
            damagable.Hit(damage);
			{}GD.Print(body.Name);
        }
		}
	public override void _Ready()
	{
		Monitoring=false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
