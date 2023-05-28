using Godot;
using System;

public partial class LoadingScreen : Control
{
    [Export] public float MoveSpeed = 70f;

    private TextureProgressBar _textureProgressBar;
    private TextureRectScript _textureRect;
    private Sprite2D _barSprite;
    private string[] _backgroundImages;
    private Random _random;

    public LoadingScreen()
    {
        _random = new Random();
        _backgroundImages = new string[]{
            "res://Art/OurArts/photo_2023-03-30_21-07-10.jpg",
            "res://Art/OurArts/скронева епілепсія.png"
        };
    }

    public override void _Ready()
    {
        _textureProgressBar = GetNode<TextureProgressBar>("TextureProgressBar");
        _textureRect = GetNode<TextureRectScript>("TextureRect");
        _barSprite = GetNode<Sprite2D>("BarSprite");

        _barSprite.Position = new Vector2(_textureProgressBar.Position.X - 20, _textureProgressBar.Position.Y - 30);

        SetRandomBackgroundImage();
    }

    public override void _Process(double delta)
    {
        if (_textureProgressBar.Value < 100)
        {
            _textureProgressBar.Value += MoveSpeed * delta;
            if (_barSprite.Position.X < _textureProgressBar.Position.X + _textureProgressBar.Size.X)
            {
                _barSprite.Position = new Vector2(_barSprite.Position.X + MoveSpeed * _textureProgressBar.Size.X * (float)delta / 100, _barSprite.Position.Y);
            }
        }
        else
        {

            Transaction.Getsingleton.Transact("res://Menu/menu.tscn");
            ProcessMode = Node.ProcessModeEnum.Disabled;
        }

    }

    private void _on_timer_background_timeout()
    {
        SetRandomBackgroundImage();
    }
    public void SetRandomBackgroundImage()
    {
        int randomindex = _random.Next(0, _backgroundImages.Length);
        _textureRect.SetTextureFromFile(_backgroundImages[randomindex]);
    }
}