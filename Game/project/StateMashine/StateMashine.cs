using Godot;
using System.Collections.Generic;
using System;
public partial class StateMashine : Node
{
    public bool IsWorking;
    private Entity _character;
    public IState CurrentState;
    public List<IState> States;
    private AnimationTree _AnimationTree;
    public StateMashine()
    {
        IsWorking = true;
        States = new List<IState>();
    }
    public override void _Ready()
    {
        _AnimationTree = GetNode<AnimationTree>("../AnimationTree");
        
        if(!IsWorking){ throw new Exception("State Mashine doesn`t work");}

        _character = GetParent<Entity>();

        for(int i = 0; i < GetChildCount();i++)
        {
            IState temp = GetChildOrNull<IState>(i);
            if(temp != null)
            {
                temp.Character = _character;
                temp.AnimTree = _AnimationTree;
                temp.Playback = (AnimationNodeStateMachinePlayback)_AnimationTree.Get("parameters/playback");
                States.Add(temp);
            }
        }
        if(States.Count == 0)
        {
            throw new NativeMemberNotFoundException("The machine did not find the states");
        }

        CurrentState = States[0];
        CurrentState.Enter();
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
            CurrentState.NextState = States[ States.Count - 1]; // the end of the states must be die state !!;
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