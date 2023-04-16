#ifndef GDEXAMPLE_H
#define GDEXAMPLE_H

#include <godot_cpp/classes/sprite2d.hpp>

namespace godot {

class GDExample : public Sprite2D {
    GDCLASS(GDExample, Sprite2D)

private:
    float time_passed;
    float amplitude;


protected:
    static void _bind_methods();

public:
    GDExample();
    ~GDExample();

    void set_amplitude(const float amplitude);
    float get_amplitude() const;

    void _process(float delta);
};

}

#endif