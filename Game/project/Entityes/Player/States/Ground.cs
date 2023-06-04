using Godot;


public partial class Ground : Node, IState
{
    public IState NextState {get; set;}
    public Entity Character{get;set;}
    public AnimationNodeStateMachinePlayback Playback{get;set;}
    public AnimationTree AnimTree{get;set;}
    public bool CanMove  {get; set;}
    private Air _air;
    private Attack _attack;

    [Export] public float JumpVelosity = -150f;
    
    public Ground()
    {
        CanMove = true;
    }
    public override void _Ready()
    {
        _air = GetNode<Air>("../Air");
        _attack = GetNode<Attack>("../Attack");
    }
    public void Enter()
    {
        Playback.Travel("move");
        {}GD.Print("State Ground");
    }
    
    public void Update(double delta)
    {
        if(!Character.IsOnFloor()){
            NextState = _air;
        }
    }
    public void Exit()
    {
        
    }
    public void StateInput(InputEvent @event)
    {
        if(@event.IsActionPressed("jump"))
        {
            _Jump();
        }
        if(@event.IsActionPressed("attack"))
        {
            _Attack();
        }
    }
    
    private void _Jump()
    {
        Vector2 velocity =  Character.Velocity;
        velocity.Y += JumpVelosity;
        Character.Velocity = velocity;
    }

    private void _Attack()
    {
        NextState = _attack;
    }
}
