using Godot;
using System;

public partial class Animation : AnimatedSprite2D
{
    private bool _animationLock = true;

    private Player p;

    public override void _Ready()
    {
        p = GetParent<Player>();
        AnimationFinished += _on_animated_sprite_2d_animation_finished;
    }

    public override void _Process(double delta)
    {
        animationUpdate();
    }
    public void animationUpdate()
    {
        FlipH = Helper.FlipUpdate(p.Velocity.X);
        if (!_animationLock)
        {
            if (!p.IsOnFloor())
            {
                Play("jump_loop");
            }
            else
            {
                if (p.Velocity.X != 0)
                {
                    Play("run");
                }
                else
                {
                    Play("idle");
                }
            }

        }
    }
    public void Jump()
    {  
        Play("air_jump");
        _animationLock = true;
    }
    public void AirJump()
    {
        
        Play("air_jump");
        _animationLock = true;
        
    }
    public void Land()
    {
        Play("jump_end");
        _animationLock = true;
    }

    public void _on_animated_sprite_2d_animation_finished()
    {
        if (Animation == "jump_end" || Animation == "air_jump")
        {
            _animationLock = false;
        }
    }
}
