using Godot;
using System;

public partial class Weapon : Area2D
{
    [Export] public float damage;
    public bool IsCharacterAttack = false;
    public override void _Ready()
    {
        BodyEntered +=CharacterEntered;
    }

    public override void _Process(double delta)
    {
        Monitoring = IsCharacterAttack;
        Monitorable = IsCharacterAttack;
    }
    void CharacterEntered(Node2D character)
    {
        if(character.GetType().Name != "Entity"){ return;}
        Entity ent = character as Entity; 
    }
}
