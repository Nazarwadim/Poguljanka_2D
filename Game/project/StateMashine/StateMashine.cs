using Godot;
using System.Collections.Generic;
using System;
public partial class StateMashine : Node
{
    public bool IsWorking = true;

    private Entity _character;
    private AnimationPlayer _animation;
    private List<IState> _states;
    public IState CurrentState;

    
    public override void _Ready()
    {
        if(!IsWorking){ throw new Exception("State Mashine doesn`t work");}

        _character = GetParent<Entity>();
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
            }
        }

        CurrentState = _states[0];
        CurrentState.Enter();

        if(_states.Count == 0)
        {
            throw new NativeMemberNotFoundException("The machine did not find the states");
        }
        {}GD.Print( _states[_states.Count-1] is Die);
    }

    public override void _Process(double delta)
    {
        if(!IsWorking){ return;}

        if(CurrentState.NextState != null)
        {
            _SwitchStates(CurrentState.NextState);
        }
         CurrentState.Update(delta);
        
        if(_character.Health <=0)
        {
            CurrentState.NextState = _states[_states.Count-1]; // the end of the states must be die state !!;
        }
    }

    public override void _Input(InputEvent @event)
    {
        if(!IsWorking){ return;}

        CurrentState.StateInput(@event);
    }

    private void _SwitchStates(IState State)
    {
        if(!IsWorking){ return;}

        if(CurrentState != null)
        {
            CurrentState.Exit();
            CurrentState = State;
            State.NextState = null;
            State.Enter();
        }
    }
}