using Godot;
using System;

public partial class Damagable : Node
{
    [Export]
    public PackedScene HealthChangedLabel;

    [Export]
    public int health = 40;

    public override void _Ready()
    {

    }

    public void Hit(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            QueueFree();
        }
        else
        {
            OnHealthChanged(this, -damage);
        }
    }

    public void OnHealthChanged(Node node, int amountChanged)
    {
        var labelInstance = (Label)HealthChangedLabel.Instantiate();
        node.AddChild(labelInstance);
        labelInstance.Text = amountChanged.ToString();
    }
}
