extends Control

@export var health_changed_label : PackedScene
@export var damage_color : Color = Color.DARK_RED
@export var heal_color : Color = Color.DARK_GREEN
# Called when the node enters the scene tree for the first time.
func _ready():
	SignalBus.connect("on_health_changed", on_signal_health_changed)


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass

func on_signal_health_changed(node : Node, ammout_change : int):
	var label_istance : Label = health_changed_label.instantiate()
	node.add_child(label_istance)
	label_istance.text = str(ammout_change)
	
	if(ammout_change>=0):
		label_istance.modulate = heal_color
	else:
		label_istance.modulate = damage_color
