using Godot;

public static class NodeFunctions
{
    public static void DeleteChildrenInNode(Node node)
    {
        foreach (var item in node.GetChildren())
        {
            item.QueueFree();   
        }
    }
};