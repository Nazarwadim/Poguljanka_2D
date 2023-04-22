using Godot;
using System;

public partial class player : CharacterBody2D
{
	[Export] public float _speed = 90.0f;
	[Export] public float _sprintSpeed = 140.0f;
	[Export] public float _jumpVelocity = -300.0f;
	[Export] public int _countOfJump = 2;
	[Export] public float _acceleration = 1f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor()){
			velocity.Y += gravity * (float)delta;
		}
		else{
			_countOfJump = 2;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && (IsOnFloor() || _countOfJump>0))
	    {
			velocity.Y = _jumpVelocity;
			_countOfJump-=1;
	    }

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			if(Input.IsActionPressed("sprint")){
			velocity.X = direction.X * _sprintSpeed * _acceleration;
			if(_acceleration < 1.30f)
			_acceleration += 0.24f;
			}
			else{
				velocity.X = direction.X * _speed;
			}
			
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);
			_acceleration = 1f;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
