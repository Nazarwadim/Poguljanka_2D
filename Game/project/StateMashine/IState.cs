using Godot;
public interface IState
{
    IState NextState {get;set;}
    Entity Character {get;set;}
    AnimationPlayer Animation {get;set;}
    bool CanMove  {get; set;}
    void Enter();
    void Update(double delta);
    
    void StateInput(InputEvent @event);
    void Exit();
}


