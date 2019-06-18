extends Area2D

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
func _ready():
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func _on_OverlapArea_body_entered(body):
	if body.is_in_group("Player"):
		player_area_logic(body)
		
func player_area_logic(var body):
	var pos = get_parent().position
	var player_pos = body.position
	
	if player_pos.x < pos.x:
		body.horizontal_knockback(-1)
	else:
		body.horizontal_knockback(1)
		
	
