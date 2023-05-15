using Godot;
using System.Collections.Generic;

public partial class StateMashine : Node
{
    public bool CanMove = true;
    private CharacterBody2D _character;
    private AnimationPlayer _animation;
    private List<IState> _states;
    private IState _currentState;

    public override void _Ready()
    {
        _character = GetParent<CharacterBody2D>();
        _animation = GetNode<AnimationPlayer>("../AnimationPlayer");
        _states = new List<IState>();

        for(int i = 0; i < GetChildCount();i++)
        {
            IState temp = GetChildOrNull<IState>(i);
            if(temp != null)
            {
                temp.Character = _character;
                temp.Animation = _animation;
                _states.Add(temp);  
                
                {}GD.Print(temp.GetType().Name);
            }
        }

        _currentState = _states[0];

        if(_states.Count == 0)
        {
            throw new NativeMemberNotFoundException("The machine did not find the states");
        }
    }

    public override void _Process(double delta)
    {
        if(_currentState.NextState !=null)
        {
            _SwitchStates(_currentState.NextState);
        }
    }

    public void SetNextState(IState State)
    {
        State.Character = _character;
        State.Animation = _animation;
        _currentState.NextState = State;
    }

    public override void _Input(InputEvent @event)
    {
        _currentState.StateInput(@event);
    }

    private void _SwitchStates(IState State)
    {
        if(_currentState != null)
        {
            _currentState.Exit();
            _currentState = State;
            State.Enter();
        }
    }

}