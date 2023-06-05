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
		Transaction.Getsingleton.QuitTree();
	}
	private void _on_start_learn_button_pressed()
	{
		Transaction.Getsingleton.LoadGame("res://Lvls/LearnToPlay/level_learn_to_play.tscn");
	}

	private void _TempButtonMassageIsNotWork()
    {
        var labelInstance = (Label)HealthChangedLabel.Instantiate();
		labelInstance.Position = new Vector2(x: 450,y:450);
        AddChild(labelInstance);
        labelInstance.Text = "Button isn`t work";
    }
}
