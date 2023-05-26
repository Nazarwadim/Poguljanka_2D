using Godot;
using System;

public partial class LoadingScreen : Control
{
    // Called when the node enters the scene tree for the first time.
    private TextureProgressBar _textureProgressBar;
    private TextureRect _textureRect;
    [Export] public float MoveSpeed = 70f;
    private Sprite2D _barSprite;
    private Label _persentageLabel;
    private string[] _backgroundImages = new string[]
    {
        "res://Art/OurArts/photo_2023-03-30_21-07-10.jpg",
        "res://Art/OurArts/скронева епілепсія.png"
    };
    private Random random = new Random();
    public override void _Ready()
    {
        _persentageLabel = GetNode<Label>("TextureProgressBar/Persentage");
        _textureProgressBar = GetNode<TextureProgressBar>("TextureProgressBar");
        _textureRect = GetNode<TextureRect>("TextureRect");
        _barSprite = GetNode<Sprite2D>("BarSprite");
        _on_timer_background_timeout();
        _barSprite.Position = new Vector2(_textureProgressBar.Position.X - 20, _textureProgressBar.Position.Y - 30);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.

    private bool _isWorking = true;
    public override void _Process(double delta)
    {
        if(!_isWorking) return;
        if (_textureProgressBar.Value < 100)
        {
            _textureProgressBar.Value += MoveSpeed * delta;
            if(_barSprite.Position.X < _textureProgressBar.Position.X + _textureProgressBar.Size.X)
            {
                _barSprite.Position = new Vector2(_barSprite.Position.X + MoveSpeed * _textureProgressBar.Size.X * (float)delta / 100, _barSprite.Position.Y);
            }
            _persentageLabel.Text = Convert.ToString((int)_textureProgressBar.Value) + "%";
        }
        else
        {
            var transaction = GetNode<Transaction>("/root/Transaction");
            transaction.Transact("res://Menu/menu.tscn");
            _isWorking = false;
        }

    }

    public void _on_timer_background_timeout()
    {
        int randomindex = random.Next(0, _backgroundImages.Length);
        var resource = ResourceLoader.Load(_backgroundImages[randomindex]);
        if (resource is Texture texture)
        {
            _textureRect.Texture = (Texture2D)ResourceLoader.Load(_backgroundImages[randomindex]);
        }
        else
        {
            GD.PrintErr($"Failed to load texture resource: {_backgroundImages[randomindex]}");
        }
    }
}