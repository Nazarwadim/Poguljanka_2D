using Godot;
using System;

public partial class EntityScript : RigidBody2D
{
    public override void _Ready()
    {
        LinearVelocity = new Vector2(10,0);
        BodyEntered += OnPlayerEntered;
    }
    public override void _PhysicsProcess(double delta)
    {
    }
    public void OnPlayerEntered(Node player)
    {
        Console.WriteLine("  asdad");
        GD.Print(player.GetClass() + " asdad");
        if(player.GetClass() == "CharacterBody2D")
        {
            GD.Print("asda");
        };
    }
}
