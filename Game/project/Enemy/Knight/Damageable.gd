extends Node

class_name Damageable

signal on_hit(node:Node, damage_taken : int, knockback_direction : Vector2)

@export var defence : int = 25
@export var max_health : float = 200
var health_changes : float = 200
@export var health : float = 200:
	get:
		return health
	set(value):
		SignalBus.emit_signal("on_health_changed", get_parent(),value - health)
		health = value
@export var dead_animation_name : String = "dead"
@export var hit_animation_name : String = "hit"

func hit(damage,attack : int, knockback_direction : Vector2):
	health -= (damage + damage * (attack - defence)/100)
	
	emit_signal("on_hit",get_parent(),damage,knockback_direction)
	
	
func _on_animation_tree_animation_finished(anim_name):
	if(anim_name == dead_animation_name):
		get_parent().queue_free()
