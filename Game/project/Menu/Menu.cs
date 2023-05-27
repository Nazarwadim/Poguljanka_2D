using Godot;
using System;

// This class and scene is Incomplete!!!!!!!!!!
public partial class Menu : Control
{
	[Export]public PackedScene HealthChangedLabel;

	private void _on_start_button_pressed(){
		_TempButtonMassageIsNotWork();
	}
	private void _on_option_button_pressed(){
		_TempButtonMassageIsNotWork();
	}
	private void _on_quit_button_pressed(){
		GetTree().Quit();
	}
	private void _on_start_learn_button_pressed(){
		Transaction.Getsingleton.Transact("res://Lvls/learn_to_play.tscn");
	}

	private void _TempButtonMassageIsNotWork()
    {
        var labelInstance = (Label)HealthChangedLabel.Instantiate();
        AddChild(labelInstance);
		labelInstance.Position = new Vector2(495,500);
        labelInstance.Text = "Button is in work";
    }
}
