#include "camera_script2D.h"
#include <godot_cpp/core/class_db.hpp>

using namespace godot;

void CameraScript2D::_bind_methods()
{
    
    ClassDB::bind_method(D_METHOD("get_amplitude"), &CameraScript2D::get_amplitude);
    ClassDB::bind_method(D_METHOD("set_amplitude", "p_amplitude"), &CameraScript2D::set_amplitude);
    ClassDB::add_property("CameraScript2D", PropertyInfo(Variant::FLOAT, "amplitude"), "set_amplitude", "get_amplitude");

    ADD_SIGNAL(MethodInfo("position_changed", PropertyInfo(Variant::OBJECT, "node"), PropertyInfo(Variant::VECTOR2, "new_pos")));
    
}


CameraScript2D::CameraScript2D() {
    // initialize any variables here
    time_passed = 0;
    amplitude = 10;

    _camera_run_time = 0;
    _smoot_velosity = {1, 1};
}
CameraScript2D::~CameraScript2D() {
    // add your cleanup here
}


void godot::CameraScript2D::_ready()
{

    Node* parentNode = get_parent();

    if (parentNode != nullptr)  
        _player = static_cast<CharacterBody2D*>(parentNode);

    _position = get_position();
}
void CameraScript2D::_process(double delta)
{
    if(_smooth_forward) _smooth_forward_global_controller(delta);
    set_position(_position);
}


void godot::CameraScript2D::_smooth_forward_global_controller(double delta)
{
    _smooth_forward_x(delta);
    _smooth_forward_y(delta);
    _camera_run_time += (float)delta;
    _position.x += _smoot_velosity.x*(float)delta;
    _position.y += _smoot_velosity.y*(float)delta;
}
void godot::CameraScript2D::_smooth_forward_x(double delta)
{
    
}
void godot::CameraScript2D::_smooth_forward_y(double delta)
{

}




void godot::CameraScript2D::set_amplitude(const float amplitude)
{
    this->amplitude = amplitude;
}

float godot::CameraScript2D::get_amplitude() const
{
    return amplitude;
}