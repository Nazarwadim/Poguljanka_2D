using Godot;
public partial class CameraScript2D : Camera2D
{

    private float _time = 0;
	[Export]
    public bool SmoothForward = false;
    private Character_body_2d _player;
    private Vector2 _position;
    private float _positionToSlide = 100;
    double delta;
    public override void _Ready()
    {

        _player = GetParent<Character_body_2d>();
        _position = Position;
    }
    public override void _Process(double delta)
    {
        this.delta = delta;
        if (SmoothForward) CameraSmoothForward();
    }
    public void CameraSmoothForward()
    {
        // smoothing X
		
        if (_player.Velocity.X > 0 && _position.X < _player.Velocity.X/(3*Zoom.X))
        {
            _position.X += _player.Velocity.X / (400*Zoom.X * Mathf.Pow(_time, 2 / 3));
        }
        else if (_player.Velocity.X < 0 && _position.X > _player.Velocity.X/(2*Zoom.X))
        {
            _position.X += _player.Velocity.X / (400*Zoom.X* Mathf.Pow(_time, 2 / 3));
        }
        else
        {
            _time = 0;
            _position.X = Mathf.MoveToward(_position.X, 0, 1.2f*Mathf.Abs(_position.X)* (float)delta);
        }
        // smothing Y
        if (_player.Velocity.Y > 0 && _position.Y < _player.Velocity.Y/(3* Zoom.X))
        {
            _position.Y += _player.Velocity.Y / (300*Zoom.X * Mathf.Pow(_time, 2 / 3));
        }
        else if (_player.Velocity.Y < 0 && _position.Y > _player.Velocity.Y/(2* Zoom.X))
        {
            _position.Y -= _player.Velocity.Y / (300 *Zoom.X* Mathf.Pow(_time, 2 / 3));
        }
        else
        {
            _time = 0;
            _position.Y = Mathf.MoveToward(_position.Y, 0, 1.2f* Mathf.Abs(_position.Y)* (float)delta);
        }
        _time += (float)delta;
        Position = _position;
    }
	public void SetPositionToSlide(float PositionToSlide)
	{
		_positionToSlide = PositionToSlide;
	}
};