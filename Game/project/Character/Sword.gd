extends Area2D

@export var attack : int = 5
@export var damage : int = 40
@export var player : Player
@export var facing_shape : FacingCollisionShape2D

var rng = RandomNumberGenerator.new()

func _ready():
	monitoring = false
	player.connect("facing_direction_changed", _on_player_direction_changed)

func _on_body_entered(body):
	for child in body.get_children():
		if child is Damageable:
			var direction_to_damageable = (body.global_position - get_parent().global_position)
			var direction_sign = sign(direction_to_damageable.x)
			damage = rng.randf_range(28, 44)
			if(direction_sign > 0):
				child.hit(damage,attack,Vector2.RIGHT)
			elif(direction_sign < 0):
				child.hit(damage,attack, Vector2.LEFT)
			else:
				child.hit(damage,attack,Vector2.ZERO)
			
func _on_player_direction_changed(facing_right : bool):
	if(facing_right):
		facing_shape.position = facing_shape.facing_right_position
	else:
		facing_shape.position = facing_shape.facing_left_position
