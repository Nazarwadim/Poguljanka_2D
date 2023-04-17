#include "camera_script2D.h"
#include <godot_cpp/core/class_db.hpp>

using namespace godot;

void CameraScript2D::_bind_methods()
{
    
    ClassDB::bind_method(D_METHOD("get_smooth_forward"), &CameraScript2D::get_smooth_forward);
    ClassDB::bind_method(D_METHOD("set_smooth_forward", "p_smooth_forward"), &CameraScript2D::set_smooth_forward);
    ClassDB::add_property("CameraScript2D", PropertyInfo(Variant::BOOL, "smooth_forward"), "set_smooth_forward", "get_smooth_forward");
    
}


CameraScript2D::CameraScript2D() {
    // initialize any variables here

    smooth_forward = true;
    _camera_run_time = 0;
    _smoot_velosity = {0, 0};
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
    if(smooth_forward) _smooth_forward_global_controller(delta);
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
    
    if (_player->get_velocity().x > 0 && _position.x < _player->get_velocity().x/(3*get_zoom().x))
    {
        _smoot_velosity.x = _player->get_velocity().x / (7*get_zoom().x * Math::pow(_camera_run_time, 2 / 3));
    }
    else if (_player->get_velocity().x < 0 && _position.x > _player->get_velocity().x/(2*get_zoom().x))
    {
        _smoot_velosity.x = _player->get_velocity().x / (7*get_zoom().x* Math::pow(_camera_run_time, 2 / 3));
    }
    else
    {
        _camera_run_time = 0;
        _smoot_velosity.x = 0;
        _position.x = Math::move_toward(_position.x, 0, 1.2f*Math::abs(_position.x)* (float)delta);
    }
}
void godot::CameraScript2D::_smooth_forward_y(double delta)
{
    if (_player->get_velocity().y > 0 && _position.y < _player->get_velocity().y/(3* get_zoom().y))
    {
        _smoot_velosity.y = _player->get_velocity().y / (5*get_zoom().x * Math::pow(_camera_run_time, 2 / 3));
    }
    else if (_player->get_velocity().y < 0 && _position.y > _player->get_velocity().y/(2* get_zoom().y))
    {
        _smoot_velosity.y = -_player->get_velocity().y / (5 *get_zoom().x* Math::pow(_camera_run_time, 2 / 3));
    }
    else
    {
        _smoot_velosity.y = 0;
        _camera_run_time = 0;
        _position.y = Math::move_toward(_position.y, 0, 1.2f* Math::abs(_position.y)* (float)delta);
    }
}




void godot::CameraScript2D::set_smooth_forward(bool smooth_forward)
{
    this->smooth_forward = smooth_forward;
}

bool godot::CameraScript2D::get_smooth_forward() const
{
    return smooth_forward;
}