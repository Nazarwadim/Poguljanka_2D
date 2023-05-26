using Godot;


public partial class Ground : Node, IState
{
    public IState NextState {get; set;}
    public Entity Character{get;set;}
    public AnimationPlayer Animation {get;set;}
    public bool CanMove  {get; set;}
    public bool CanDash  {get; set;}

    [Export] public float JumpVelosity = -150f;
    [Export] public float DashVelosity = 1000f;
    [Export] public Vector2 direction;
    public static int lastDirection { get; set; }
    public Ground()
    {
        CanMove = true;
        CanDash = true;
    }

    private Air _air;
    private Attack _attack;

    public override void _Ready()
    {
        _air = GetNode<Air>("../Air");
        _attack = GetNode<Attack>("../Attack");
    }
    public void Enter()
    {
        {}GD.Print("State Ground");
        Animation.Play("Idle");
    }
    
    public void Update(double delta)
    {
        if(!Character.IsOnFloor()){
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
        if (direction.X < 0) 
            Ground.lastDirection = -1;
        else if (direction.X > 0)
            Ground.lastDirection = 1;

        if(@event.IsActionPressed("jump"))
        {
            _Jump();
        }
        if(@event.IsActionPressed("attack"))
        {
            _Attack();
        }
        if(@event.IsActionPressed("dash"))
        {
        _Dash(lastDirection);
        }
        direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Air air = new Air();
        Air.lastDirection = lastDirection; 
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

    private void _Dash(int i)
    {
        Vector2 velocity = Character.Velocity;
        velocity.X = i*DashVelosity;
        Character.Velocity = velocity;
    }
}
