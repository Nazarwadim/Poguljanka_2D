#ifndef GDEXAMPLE_H
#define GDEXAMPLE_H

#include <godot_cpp/classes/camera2d.hpp>
#include <godot_cpp/classes/character_body2d.hpp>

namespace godot {

class CameraScript2D : public Camera2D {
    GDCLASS(CameraScript2D, Camera2D)

private:
    Vector2 _smoot_velosity;
    Vector2 _position;

    float _camera_run_time;
    CharacterBody2D *_player;

    void _smooth_forward_global_controller(double delta);
    void _smooth_forward_x(double delta);
    void _smooth_forward_y(double delta);
protected:
    static void _bind_methods();

public:
    CameraScript2D();
    ~CameraScript2D();

    bool smooth_forward;

    void _ready() override;
    void _process(double delta) override;

    void set_smooth_forward(bool smooth_forward);
    bool get_smooth_forward() const;



};

}

#endif