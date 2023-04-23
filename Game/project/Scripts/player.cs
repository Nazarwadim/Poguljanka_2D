using Godot;
using System;

public partial class player : CharacterBody2D
{
    [Export] public float _speed;
    [Export] public float _dashSpeed;
    [Export] public float _jumpVelocity = -300.0f;
    [Export] public float _airJumpVelocity = -222.0f;
    [Export] public int _countOfJump = 2;
    [Export] public float _acceleration = 1f;
	public float _dashTimer = 0.15f;
	public float _dashTimerReset = 0.15f;

    public Vector2 direction = Vector2.Zero;
    public Vector2 velocity = new Vector2();

    AnimatedSprite2D _animatedSprite2D = new AnimatedSprite2D();
	[Export] public PackedScene GhostPlayer;

    public bool AnimationLock = true;
    public bool WasInAir = false;
    public bool IsDashing = false;
	
    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity.Y += gravity * (float)delta;
            WasInAir = true;
        }
        else
        {
            _countOfJump = 2;
            if (WasInAir == true)
            {
                Land();
            }
            WasInAir = false;
        }


        // Handle Jump.
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            Jump();
            _countOfJump--;
        }
        else if (Input.IsActionJustPressed("ui_accept") && _countOfJump > 0)
        {
            AirJump();
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        if (!IsDashing)
        {
            direction = Input.GetVector("left", "right", "up", "down");
            if (direction.X != 0 && _animatedSprite2D.Animation != "jump_end")
            {
                velocity.X = direction.X * _speed;
            }
            else
            {
                velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);
            }
        }
        if (Input.IsActionJustPressed("dash"))
        {
            if (Input.IsActionPressed("left"))
            {
                velocity.X = -_dashSpeed;
				IsDashing = true;
				
            }
            else if (Input.IsActionPressed("right"))
            {
                velocity.X = _dashSpeed;
				IsDashing = true;
            }
			GhostPlayer _ghost = GhostPlayer.Instantiate() as GhostPlayer;
			Owner.AddChild(_ghost);
			_ghost.GlobalPosition = this.GlobalPosition;
			_ghost.SetHValue(_animatedSprite2D.FlipH);
			_dashTimer = _dashTimerReset;
        }
		if(IsDashing)
		{
			_dashTimer -= (float)delta;
			if(_dashTimer <= 0 ){
				IsDashing = false;
			}
		}

        Velocity = velocity;
        MoveAndSlide();
        animationUpdate();
        FlipUpdate();
    }
    public void animationUpdate()
    {
        if (!AnimationLock)
        {
            if (!IsOnFloor())
            {
                _animatedSprite2D.Play("jump_loop");
            }
            else
            {
                if (direction.X != 0)
                {
                    _animatedSprite2D.Play("run");
                }
                else
                {
                    _animatedSprite2D.Play("idle");
                }
            }

        }
    }
    public void FlipUpdate()
    {
        if (direction.X > 0)
        {
            _animatedSprite2D.FlipH = false;
        }
        else if (direction.X < 0)
        {
            _animatedSprite2D.FlipH = true;
        }
    }
    public void Jump()
    {
        velocity.Y = _jumpVelocity;
        _animatedSprite2D.Play("air_jump");
        AnimationLock = true;
    }
    public void AirJump()
    {
        velocity.Y = _airJumpVelocity;
        _animatedSprite2D.Play("air_jump");
        AnimationLock = true;
        _countOfJump--;
    }
    public void Land()
    {
        _animatedSprite2D.Play("jump_end");
        AnimationLock = true;

    }
    public void _on_animated_sprite_2d_animation_finished()
    {
        if (_animatedSprite2D.Animation == "jump_end")
        {
            AnimationLock = false;
        }
        else if (_animatedSprite2D.Animation == "jump_end")
        {
            AnimationLock = false;
        }
        else if (_animatedSprite2D.Animation == "air_jump")
        {
            AnimationLock = false;
        }

    }
}