#include "gdexample.h"
#include <godot_cpp/core/class_db.hpp>

using namespace godot;



void GDExample::_bind_methods()
{
    ClassDB::bind_method(D_METHOD("get_amplitude"), &GDExample::get_amplitude);
    ClassDB::bind_method(D_METHOD("set_amplitude", "p_amplitude"), &GDExample::set_amplitude);
    ClassDB::add_property("GDExample", PropertyInfo(Variant::FLOAT, "amplitude"), "set_amplitude", "get_amplitude");

    ADD_SIGNAL(MethodInfo("position_changed", PropertyInfo(Variant::OBJECT, "node"), PropertyInfo(Variant::VECTOR2, "new_pos")));
    
}

GDExample::GDExample() {
    // initialize any variables here
    time_passed = 0;
    amplitude = 10;
}

GDExample::~GDExample() {
    // add your cleanup here
}

void GDExample::_process(float delta) {
    time_passed += delta;

    Vector2 new_position = Vector2(10.0 + (amplitude * sin(time_passed * 2.0)), 10.0 + (amplitude * cos(time_passed * 1.5)));

    set_position(new_position);

    if(time_passed > 1)
    {
        emit_signal("position_changed", this, new_position);
    }
}

void godot::GDExample::set_amplitude(const float amplitude)
{
    this->amplitude = amplitude;
}

float godot::GDExample::get_amplitude() const
{
    return amplitude;
}