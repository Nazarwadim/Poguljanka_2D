using Godot;
using System;

public partial class TextureRectScript : TextureRect
{
    public void SetTextureFromFile(string fileName)
    {
        var resource = ResourceLoader.Load(fileName);
        if (resource is Texture texture)
        {
            Texture = (Texture2D)ResourceLoader.Load(fileName);
        }
        
        else
        {
            GD.PrintErr($"Failed to load texture resource: {fileName}");
        }
    }
}
