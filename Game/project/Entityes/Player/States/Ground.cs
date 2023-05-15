using Godot;


public partial class Ground : Node, IState
{
    public IState NextState {get; set;}
    public CharacterBody2D Character{get;set;}
    public AnimationPlayer Animation {get;set;}

    private StateMashine _mashine;
    private Timer _dalay;

    public override void _Ready()
    {
        _dalay = GetNode<Timer>("Dalay");
    }
    public void Enter()
    {
        _mashine.CanMove = true;
    }
    
    public void Update(double delta)
    {
        if(!Character.IsOnFloor() && _dalay.IsStopped())
        {
            _mashine.SetNextState(new Air());
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
                
    }

    private void _Attack()
    {
        _mashine.SetNextState(new Attack());
        Animation.Play("attack_1");
    }
}
