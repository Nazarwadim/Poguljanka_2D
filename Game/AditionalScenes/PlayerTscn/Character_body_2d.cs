using Godot;
public partial class Character_body_2d : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	private AnimatedSprite2D _playerAnimation;
	private CameraScript2D _camera2D;
	public override void _Ready()
	{
		_camera2D = GetNode<CameraScript2D>("Camera2D");
		_playerAnimation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_playerAnimation.Animation = "default";
	}
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			_playerAnimation.Animation = "run";
			_playerAnimation.FlipH = direction.X < 0;
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, 100);
		}
		if(Velocity.X == 0)
		{
			_playerAnimation.Animation = "default";
		}
		Velocity = velocity;
		MoveAndSlide();
		_playerAnimation.Play();
	}

};
