using Godot;
using System;

public partial class box : Node2D
{
    public AnimatedSprite2D _animationPlayer;
	// Called when the node enters the scene tree for the first time.
    private Node2D Player;
    private bool contains_player = false;
    private bool can_leave_box = false;
    private AudioStreamPlayer2D audioStreamPlayer2D;


	public override void _Ready()
	{
        audioStreamPlayer2D = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        Player = GetTree().Root.GetNode<Node2D>("MainMenu/Game/Level01/RigidBody2D/Player");   
        GetNode<Node2D>("Node2D").Position = new Vector2(0, 2);
        _animationPlayer = GetNode<AnimatedSprite2D>("Node2D/AnimatedSprite2D");
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if(GameManager.GlobalGameManager.touching_box && !contains_player)
        {
            if(Input.IsActionJustPressed("interact") && GetInstanceId() == GameManager.GlobalGameManager.box_id)
            {
                GD.Print("interact pressed 1");
                Player.Visible = false;
                _animationPlayer.Play("box_peeking");
                audioStreamPlayer2D.Play();
                GetNode<Node2D>("Node2D").Position = new Vector2(0, 0);
                contains_player = true;
                can_leave_box = false;
            }
        }
        if(contains_player && can_leave_box){
            if(Input.IsActionJustPressed("interact"))
            {
                GD.Print("interact pressed 2");
                Player.Visible = true;
                _animationPlayer.Play("box_idle");
                // Print box instance id
                GD.Print("box instance id: " + GetInstanceId());
                GetNode<Node2D>("Node2D").Position = new Vector2(0, 2);
                contains_player = false;
            }
        }
	}

    public void _on_area_2d_body_entered(Node2D body)
    {
        if(body is player)
        {
            GD.Print("body entered");
            GameManager.GlobalGameManager.box_id = GetInstanceId();
            GameManager.GlobalGameManager.touching_box = true;
        }
        GD.Print("touching box: " + GameManager.GlobalGameManager.touching_box);
    }

    public void _on_area_2d_body_exited(Node2D body)
    {
        if(body is player)
        {
            GD.Print("body exited");
            GameManager.GlobalGameManager.touching_box = false;
        }
        GD.Print("touching box: " + GameManager.GlobalGameManager.touching_box);
    }

    public void _on_animated_sprite_2d_frame_changed()
    {
        can_leave_box = true;
    }
}
