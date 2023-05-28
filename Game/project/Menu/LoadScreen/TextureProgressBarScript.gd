extends TextureProgressBar
@onready var persentage = $Persentage

func _process(delta):
	var int_value = int(value)
	persentage.text =str(int_value) + "%"
