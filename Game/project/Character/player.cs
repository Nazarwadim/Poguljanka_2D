using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] public float _speed;
    [Export] public float _dashSpeed;
    [Export] public float _jumpVelocity = -300.0f;
    [Export] public float _airJumpVelocity = -222.0f;
    [Export] public int _countOfJump = 2;
    [Export] public float _acceleration = 1f;



    private Vector2 direction = Vector2.Zero;
    private Vector2 velocity = Vector2.Zero;


    private Animation _animatedSprite2D;
	[Export] public PackedScene GhostPlayer;

    public bool WasInAir = false;
    public bool IsDashing = false;
	
    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
        _animatedSprite2D = GetNode<Animation>("AnimatedSprite2D");
        GetNode<Timer>("Timer").Timeout += _DashEnd;
    }

    public override void _PhysicsProcess(double delta)
    {
        velocity = Velocity;        
        
        

        _ControlUpdate();
        _PlayerDashing(delta);
        _Gravitation(delta);
        MoveAndSlide();


        Velocity = velocity;
    }

    private void _ControlUpdate()
    {
        if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
        {
            _Jump();
            _countOfJump--;
        }
        else if (Input.IsActionJustPressed("ui_accept") && _countOfJump > 0)
        {
            _AirJump();
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
    }
    private void _Jump()
    {
        velocity.Y = _jumpVelocity;
        _animatedSprite2D.Jump();
    }
    private void _AirJump()
    {
        velocity.Y = _airJumpVelocity;
        _animatedSprite2D.AirJump();
        _countOfJump--;
    }

    private void _PlayerDashing(double delta)
    {
        if (Input.IsActionJustPressed("dash"))
            _PlayerDashingStart();
		if(IsDashing)
		{
            if(velocity.X > 0)
            {
                GetNode<AnimationPlayer>("GhostPlayerDash/Anim").Play("left");
            }
            else{
                GetNode<AnimationPlayer>("GhostPlayerDash/Anim").Play("right");
            }
		}
        GetNode<CanvasGroup>("GhostPlayerDash").Visible = IsDashing;
    }
    private void _PlayerDashingStart()
    {
        if(direction.X == 0) return;

        GetNode<Timer>("Timer").Start();
        IsDashing = true;

        if (direction.X < 0)
        {
            velocity.X = -_dashSpeed;
			return;
        }

        velocity.X = _dashSpeed;
    }

    private void _DashEnd()
    {
        IsDashing = false;
    }

    private void _Gravitation(double delta)
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
                _animatedSprite2D.Land();
            }
            WasInAir = false;
        }
    }
};