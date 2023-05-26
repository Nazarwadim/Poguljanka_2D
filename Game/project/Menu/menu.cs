using Godot;
using System;

public partial class menu : Control
{
	[Export]
    public PackedScene HealthChangedLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_start_button_pressed(){
		ButtonInWork(this);
	}

	private void _on_option_button_pressed(){
		ButtonInWork(this);
	}

	private void _on_quit_button_pressed(){
		GetTree().Quit();
	}
	private void _on_start_learn_button_pressed(){
		var transaction = GetNode<Transaction>("/root/Transaction");
		transaction.transact("Lvls/learn_to_play.tscn");
	}

	public void ButtonInWork(Node node)
    {
        var labelInstance = (Label)HealthChangedLabel.Instantiate();
        node.AddChild(labelInstance);
		labelInstance.Position = new Vector2(495,500);
        labelInstance.Text = "Button is in work";
    }

}
