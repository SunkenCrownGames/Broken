extends KinematicBody2D

# Declare member variables here. Examples:
# var a = 2
# var b = "text"

# Called when the node enters the scene tree for the first time.
#func _ready():
	#pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

export (int) var horizontal_acceleration = 750
export (int) var max_horizontal_speed = 3000
export (int) var horizontal_speed_deadzone = 500

export (int) var gravity = 7500
export (int) var jump_height = 3500
export (int) var max_jump_count = 2

var velocity = Vector2()
#Horizontal Data
var horizontal_speed = float()
var direction = float()

#Vertical Data
var jump = false
var jump_count = 0
var vertical_speed = float()

#Hit Data
var hit_direction = HitDirection.RIGHT
onready var under_ray = get_node("Under_ray") 

var ground = null

#Gets the User Input
func get_input():
	if Input.is_action_pressed('right'):
		direction = 1
	elif Input.is_action_pressed('left'):
		direction = -1
	else:
		direction = 0
	
func _process(delta):
	get_input()
	movement_logic(delta)

#physics update loop
func _physics_process(delta):
	velocity = move_and_slide(velocity, Vector2(0,-1))
	collision()

func movement_logic(delta):
	horizontal_movement(delta)
	jump()
	gravity(delta)
	build_movement_vector()


func horizontal_movement(delta):
	if horizontal_speed < max_horizontal_speed && direction == 1:
		horizontal_speed += horizontal_acceleration * direction
	elif horizontal_speed > -max_horizontal_speed && direction == -1:
		horizontal_speed += horizontal_acceleration * direction
		
	elif horizontal_speed <= -horizontal_speed_deadzone:
		horizontal_speed += horizontal_acceleration
		
	elif horizontal_speed >= horizontal_speed_deadzone:
		horizontal_speed -= horizontal_acceleration
		
	if horizontal_speed > -horizontal_speed_deadzone && horizontal_speed < horizontal_speed_deadzone && direction == 0:
		horizontal_speed = 0

#Vertical Logic
func jump():
	if Input.is_action_just_pressed("jump") &&  jump_count < max_jump_count:
		jump = true
		jump_count += 1
		vertical_speed = -jump_height
		
func gravity(delta):
	if !is_on_floor():
		vertical_speed += gravity * delta

func build_movement_vector():
	velocity.x = horizontal_speed
	velocity.y = vertical_speed
		
#Collision Logic
func collision():
	if is_on_floor():
		#print("Grounded")
		groundReset()
	
	for i in get_slide_count():
		var collision = get_slide_collision(i)
		
		if is_on_floor() && collision.collider.is_in_group("Ground"):
			if ground != collision.collider:
				ground = collision.collider
				print("GROUND CHANGED")
		
	if is_on_ceiling():
		vertical_speed = 0

func groundReset():
	if jump == false:
		vertical_speed = 0
	else:
		jump = false
	jump_count = 0
	
func horizontal_knockback(h_dir):
	horizontal_speed = 5000 * h_dir

	
enum MovementStatus {GROUNDED, JUMPING, FALLING, NONE}
enum HitDirection {LEFT,RIGHT}

