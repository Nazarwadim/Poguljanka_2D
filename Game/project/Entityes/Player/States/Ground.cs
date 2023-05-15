using Godot;


public partial class Ground : Node, IState
{
    public IState NextState {get; set;}
    public CharacterBody2D Character{get;set;}
    public AnimationPlayer Animation {get;set;}

    [Export] public float JumpVelosity = -150f;
    public StateMashine Mashine {get;set;}

    private Timer _dalay;
    private Air _air;
    private Attack _attack;

    public override void _Ready()
    {
        _dalay = GetNode<Timer>("Dalay");
        _air = GetNode<Air>("../Air");
        _attack = GetNode<Attack>("../Attack");
    }
    public void Enter()
    {
        GD.Print("State Ground");
        Animation.Play("Idle");
        Mashine.CanMove = true;
    }
    
    public void Update(double delta)
    {
        if(Character.IsOnFloor() == false)
        {
            NextState = _air;
        }
        if(Character.Velocity.X != 0){
            Animation.CurrentAnimation = "run";
        }
        else{
            
            Animation.CurrentAnimation = "Idle";
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
