using Godot;


static class Helper{
    public static bool FlipUpdate(float direction, bool before)
    {
        if(direction > 0) return false;
        if(direction < 0) return true;
        return before;
    }


}