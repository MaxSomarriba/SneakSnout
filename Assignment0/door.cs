using Godot;
using System;

public partial class door : Node2D
{
    public AnimatedSprite2D _animationPlayer;
    private Node2D Player;
    private AudioStreamPlayer2D audioStreamPlayer2D;
    private Control winScreen;
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        Player = GetTree().Root.GetNode<Node2D>("MainMenu/Game/Level01/RigidBody2D/Player");   
        _animationPlayer = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        audioStreamPlayer2D = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        winScreen = GetTree().Root.GetNode<Control>("MainMenu/WinScreen");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void _on_area_2d_body_entered(Node2D body)
    {
        if(body is player)
        {
            GD.Print("body entered");
            _animationPlayer.Play("door_open");
            audioStreamPlayer2D.Play();
            GameManager.GlobalGameManager.level_complete = true;
            winScreen.Visible = true;
            if(GameManager.GlobalGameManager.Score == 1)
            {
                winScreen.GetNode<Label>("Label").Text = "You won with " + GameManager.GlobalGameManager.Score + " diamond!";
            }
            else
            {
                winScreen.GetNode<Label>("Label").Text = "You won with " + GameManager.GlobalGameManager.Score + " diamonds!";
            }            
        }
    }

    public void _on_area_2d_body_exited(Node2D body)
    {
    }
}
