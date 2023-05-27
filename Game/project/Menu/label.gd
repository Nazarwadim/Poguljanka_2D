extends Label

@export var speed:Vector2

func _process(delta):
	position += speed * delta

func _on_timer_timeout():
	queue_free()
