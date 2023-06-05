extends Button

var text_scene:PackedScene = preload("res://UI/MainMenu/label.tscn")
func _on_pressed():
	var label:Label = text_scene.instantiate()
	label.text = "This button isn`t work"
	label.position.x -= 25
	label.position.y += 150
	add_child(label)

