using Godot;
using System;

public partial class diamond_pickup : Node2D
{
    public int ScoreGainAmount = 1; 
    public AudioStreamPlayer2D audioStreamPlayer2D;
    public GameManager game;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        audioStreamPlayer2D = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
	}

    public void _on_area_2d_body_entered(Node2D body)
    {
        GD.Print("body entered");

        // Change animation
        GetNode<AnimatedSprite2D>("Node2D/AnimatedSprite2D").Play("diamond_hit");
        audioStreamPlayer2D.Play();
        // QueueFree();
    }

    public void _on_animated_sprite_2d_animation_finished(){
        GD.Print("animation finished");
        // GameManager.AddScore(ScoreGainAmount);
        game = GetTree().Root.GetNode<GameManager>("MainMenu/Game");
        Godot.Collections.Array<Godot.Node> children = GetTree().Root.GetChildren();
        foreach(Node child in children){
            GD.Print("Child Node name " + child.Name);
        }

        game.AddScore(ScoreGainAmount);
        
        GD.Print(game.Score);
        QueueFree();
    }

    // public void _on_body_exited(object body){
        
    // }


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
