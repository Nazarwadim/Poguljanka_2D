extends Node2D

class_name LevelManager
@export var player:CharacterBody2D

func _on_child_entered_tree(node:Level):
	if player == null:
		printerr("Player is null at Path: ", get_path())
		pass
	player.position.x = node.spawn_player_position.x
	player.position.y = node.spawn_player_position.y
