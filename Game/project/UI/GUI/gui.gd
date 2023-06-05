extends CanvasLayer
@export var game: Node

func _input(event: InputEvent):
	if event.is_action_pressed("Escape"):
		if visible == false:
			game.process_mode = Node.PROCESS_MODE_DISABLED	
		else:
			game.process_mode = Node.PROCESS_MODE_INHERIT
		visible = not visible
