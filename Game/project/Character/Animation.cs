using Godot;
using System;

public partial class Animation : AnimatedSprite2D
{
    private bool _animationLock = false;

    private Player p;

    public enum State
    {
        jump_loop,
        run,
        idle,
        jump_start,
        air_jump,
        jump_end,
        attack
    }

    State state = State.idle;
    public override void _Ready()
    {
        p = GetParent<Player>();
    }

    public override void _Process(double delta)
    {
        
        if(state == State.air_jump) Play("air_jump");

        else if(state == State.jump_loop) Play("jump_loop");

        else if(state == State.run) Play("run");

        else if(state == State.idle) Play("idle");

        else if(state == State.jump_start) Play("jump_start");

        else if(state == State.jump_end) Play("jump_end");

        else if(state == State.attack) Play("attack");

        _BaseAnimationUpdate();
    }
    private void _BaseAnimationUpdate()
    {
        FlipH = Helper.FlipUpdate(p.Velocity.X, FlipH);

        if (!_animationLock)
        {
            if (p.Velocity.Y < -1)
            {
                state = State.jump_loop;
            }

            else if(p.Velocity.X != 0)
            {
                state = State.run;
            }
            else{
                state = State.idle;
            }
        }
    }
    async public void Jump()
    {  
        _animationLock = true;
        state = State.jump_start;
        await ToSignal(GetTree().CreateTimer(0.5), "timeout");
        _animationLock = false;
    }
    async public void AirJump()
    { 
        _animationLock = true;
        state = State.air_jump;   
         await ToSignal(GetTree().CreateTimer(0.3), "timeout");
        _animationLock = false;
    }
    async public void Land()
    {
        _animationLock = true;
        state = State.jump_end;        
        await ToSignal(GetTree().CreateTimer(0.6), "timeout");
        _animationLock = false;
    }

    async public void Attack()
    {
        _animationLock = true;
        state = State.attack;
        await ToSignal(GetTree().CreateTimer(0.12), "timeout");
        _animationLock = false;
    }
}
