using Godot;
public interface IState
{
    IState NextState {get;set;}
    CharacterBody2D Character {get;set;}
    AnimationPlayer Animation {get;set;}
    public StateMashine Mashine {get;set;}
    void Enter();
    void Update(double delta);
    
    void StateInput(InputEvent @event);
    void Exit();
}


