extends KinematicBody2D

export (int) var movement_speed = 500
export (int) var gravity_scale = 7500

#Attack Ranges
export (int) var melee_range = 550

#Horizontal Data
var horizontal_speed = float()
var horizontal_direction = 1

#Vertical Data
var vertical_speed = float()

#Detection Data
onready var left_ray = get_node("LeftRange")
onready var right_ray = get_node("RightRange")
onready var chase_timer = get_node("ChaseTimer")

var ground = null
var player = null

#Range Data
var in_range = false
var current_range = 0


var velocity = Vector2()

# Called when the node enters the scene tree for the first time.
func _ready():
	set_meta("type", "enemy")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	movement_logic(delta)
	
	if in_range && player.is_on_floor():
			var knockback_dir = track_hit_direction()
			player.horizontal_knockback(knockback_dir)
	
# warning-ignore:unused_argument
func _physics_process(delta):
	velocity = move_and_slide(velocity, Vector2(0,-1))
	collision()

func movement_logic(delta):
	gravity(delta)	
	update_ray()
	
	if player == null:
		patrol_logic()
		detection()
	else:
		chase(delta)
			
	build_movement_vector()
	
func gravity(delta):
	if !is_on_floor():
		vertical_speed += gravity_scale * delta
		
func build_movement_vector():
	velocity.x = horizontal_speed
	velocity.y = vertical_speed
	
func collision():
	for i in get_slide_count():
		var collision = get_slide_collision(i)
		
		if is_on_floor():
			if ground == null:
				ground = collision.collider
		
		if collision.collider.is_in_group("LeftLimit"):
			direction_switch(1)
		
		if collision.collider.is_in_group("RightLimit"):
			direction_switch(-1)
			
func detection():
	var collider = null
	if horizontal_direction == 1 && right_ray.is_colliding() :
		collider = right_ray.get_collider()
	elif horizontal_direction == -1 && left_ray.is_colliding() :
		collider = left_ray.get_collider()
	
	if collider != null && collider.is_in_group("Player"):
		#print("PLAYER")
		player = collider
		horizontal_speed = 0

func update_ray():
	if horizontal_direction == 1:
		left_ray.enabled = false
		right_ray.enabled = true
	else:
		left_ray.enabled = true
		right_ray.enabled = false

func direction_switch(dir):
	horizontal_direction = dir
			
func patrol_logic():
	horizontal_speed = movement_speed * horizontal_direction

# warning-ignore:unused_argument
func chase(delta):
	
	#Change direciton depending on player position
	horizontal_direction = track_hit_direction()
		
	var distance = abs(player.position.x - position.x)
	
	if distance >= melee_range:
		
		if in_range == true && chase_timer.is_stopped():
			#print("START TIMER")
			chase_timer.start()
		
		if in_range == false:
			horizontal_speed = movement_speed * horizontal_direction
	else:
		horizontal_speed = 0
		in_range = true
		
		
	#If the player leaves the platform
	if player.ground != ground && player.ground != null:
		#print("Player Left Platform Go Back to patrolling")
		player = null
	

func track_hit_direction():
	if player != null:
		var dir = 0
		if player.position.x < position.x:
			dir = -1
		else:
			dir = 1
		
		return dir
	return 0

func _on_ChaseTimer_timeout():
	in_range = false
