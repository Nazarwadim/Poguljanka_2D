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


    private Vector2 direction = Vector2.Zero;
    private Vector2 velocity = Vector2.Zero;


    private AnimatedSprite2D _animatedSprite2D;
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


        playerDashing(delta);
        gravitation(delta);
        Velocity = velocity;
        MoveAndSlide();
        animationUpdate();
    }

    private void playerDashing(double delta)
    {
        if (Input.IsActionJustPressed("dash"))
            _PlayerDashingStart();
		if(IsDashing)
		{
            GetNode<AnimationPlayer>("GhostPlayerDash/Anim").Play("ToLowM");
            
			_dashTimer -= (float)delta;

			if(_dashTimer <= 0 )
            {
				IsDashing = false;
			}
		}
        GetNode<CanvasGroup>("GhostPlayerDash").Visible = IsDashing;
    }
    private void _PlayerDashingStart()
    {
        if (direction.X < 0)
            {
                velocity.X = -_dashSpeed;
				IsDashing = true;
				
            }
            else if(direction.X > 0)
            {
                velocity.X = _dashSpeed;
				IsDashing = true;
            }
		_dashTimer = _dashTimerReset;
    }
    public void animationUpdate()
    {
        _animatedSprite2D.FlipH = Helper.FlipUpdate(direction.X);
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
        if (_animatedSprite2D.Animation == "jump_end" || _animatedSprite2D.Animation == "air_jump")
        {
            AnimationLock = false;
        }
    }
    private void gravitation(double delta)
    {
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
    }
};