using Godot;
public partial class CameraScript2D : Camera2D
{
	[Export]
    public bool SmoothForward;

    private Vector2 _smoothVelosity;
    private float _cameraRunTime; 
    private Character_body_2d _player;
    private Vector2 _position; //we create to get gloabal position

    public override void _Ready()
    {
        _smoothVelosity = new Vector2();
        _player = GetParent<Character_body_2d>();
        _cameraRunTime = 0;
        _position = Position;
    }

    public override void _Process(double delta)
    {
        if (SmoothForward)  SmoothForwardGlobalController(delta);  
        Position = _position;
    }


    private void SmoothForwardGlobalController(double delta)
    {
        SmoothForwardX(delta);
        SmoothForwardY(delta);
         _cameraRunTime += (float)delta;
        _position.X +=_smoothVelosity.X*(float)delta;
        _position.Y +=_smoothVelosity.Y*(float)delta;
    }

    private void SmoothForwardX(double delta)
    {
        if (_player.Velocity.X > 0 && _position.X < _player.Velocity.X/(3*Zoom.X))
        {
            _smoothVelosity.X = _player.Velocity.X / (7*Zoom.X * Mathf.Pow(_cameraRunTime, 2 / 3));
        }
        else if (_player.Velocity.X < 0 && _position.X > _player.Velocity.X/(2*Zoom.X))
        {
            _smoothVelosity.X = _player.Velocity.X / (7*Zoom.X* Mathf.Pow(_cameraRunTime, 2 / 3));
        }
        else
        {
            _cameraRunTime = 0;
            _smoothVelosity.X = 0;
            _position.X = Mathf.MoveToward(_position.X, 0, 1.2f*Mathf.Abs(_position.X)* (float)delta);
        }
    }
    
    private void SmoothForwardY(double delta)
    {
        if (_player.Velocity.Y > 0 && _position.Y < _player.Velocity.Y/(3* Zoom.X))
        {
            _smoothVelosity.Y = _player.Velocity.Y / (5*Zoom.X * Mathf.Pow(_cameraRunTime, 2 / 3));
        }
        else if (_player.Velocity.Y < 0 && _position.Y > _player.Velocity.Y/(2* Zoom.X))
        {
            _smoothVelosity.Y = -_player.Velocity.Y / (5 *Zoom.X* Mathf.Pow(_cameraRunTime, 2 / 3));
        }
        else
        {
            _smoothVelosity.Y = 0;
            _cameraRunTime = 0;
            _position.Y = Mathf.MoveToward(_position.Y, 0, 1.2f* Mathf.Abs(_position.Y)* (float)delta);
        }
    }
};