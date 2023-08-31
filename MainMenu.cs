using Godot;
using System;

public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void _on_button_pressed()
    {
        
        // Print Main Menu children
        Godot.Collections.Array<Godot.Node> children = GetTree().Root.GetNode<Control>("MainMenu").GetChildren();
        foreach(Node child in children){
            GD.Print("Child Node name " + child.Name);
        }

        GetTree().Root.GetNode<Node2D>("MainMenu/Game").Visible = true;
    }

    public void _on_button_pressed_play_again()
    {
        // Reload the scene
        // GetTree().Root.GetNode<Node2D>("MainMenu/Game").QueueFree();
        // PackedScene game = GD.Load<PackedScene>("res://game.tscn");
        // Node instance = game.Instantiate();
        // instance.Name = "Game";
        // GD.Print("instance name: " + instance.Name);
        // AddChild(instance);
        // GetTree().Root.GetNode<Node2D>("MainMenu/Game").Visible = true;
        // GetTree().Root.GetNode<Control>("MainMenu/PlayAgainMenu").Visible = false;
        GetTree().ReloadCurrentScene();
        GameManager.GlobalGameManager.level_complete = false;
        GameManager.GlobalGameManager.Score = 0;
        GameManager.GlobalGameManager.pig_found = false;
        GameManager.GlobalGameManager.level_complete = false;
        GameManager.GlobalGameManager.touching_box = false;
        GameManager.GlobalGameManager.box_id = 0;
    }
}
