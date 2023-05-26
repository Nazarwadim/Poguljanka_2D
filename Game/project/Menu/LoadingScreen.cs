using Godot;
using System;

public partial class LoadingScreen : Control
{
	// Called when the node enters the scene tree for the first time.
	private ProgressBar progressBar;
	private TextureProgressBar textureProgressBar;
	private TextureRect textureRect;
	private float moveSpeed = 70f;
	private float targetX = 950f;
	private Sprite2D barSprite;
	private string[] backgroundImages = new string[]
    {
        "res://Art/OurArts/photo_2023-03-15_21-47-55.jpg",
        "res://Art/OurArts/photo_2023-03-30_21-07-10.jpg",
        "res://Art/OurArts/скронева епілепсія.png"
    };
    private Random random = new Random();
	public override void _Ready()
	{
		progressBar = GetNode<ProgressBar>("ProgressBar");
		textureProgressBar = GetNode<TextureProgressBar>("TextureProgressBar");
		textureRect = GetNode<TextureRect>("TextureRect");
		barSprite = GetNode<Sprite2D>("BarSprite");
		barSprite.Position = new Vector2(250,318);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void _on_timer_timeout(){
		progressBar.Value+=1;
		textureProgressBar.Value+=1;

		if(progressBar.Value>=100) {
		var transaction = GetNode<Transaction>("/root/Transaction");
		transaction.transact("C://Games/OurProject/Game/project/Menu/menu.tscn");
		}
	}

	public void _on_timer_background_timeout(){
		int randomindex = random.Next( 0 , backgroundImages.Length);
        var resource = ResourceLoader.Load(backgroundImages[randomindex]);
		if (resource is Texture texture)
        {
			textureRect.Texture = (Texture2D)ResourceLoader.Load(backgroundImages[randomindex]);
        }
        else
        {
            GD.PrintErr($"Failed to load texture resource: {backgroundImages[randomindex]}");
        }
	}

    public override void _PhysicsProcess(double delta)
    {
		var mousePos = GetGlobalMousePosition();
        barSprite.Position += new Vector2(moveSpeed * (float)delta, 0);
 		if (barSprite.Position.X >= targetX)
        {
            barSprite.Position = new Vector2(targetX, barSprite.Position.Y);
        }
    }
}
