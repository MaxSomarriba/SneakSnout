using Godot;
using System;

public partial class king : Node2D
{
    private int speed = 15;
    public AnimatedSprite2D _animatedSprite2D;
    private RigidBody2D PlayerRigidBody2D;
    private Node2D Player;
    private CharacterBody2D _characterBody2D;
    private AudioStreamPlayer2D audioStreamPlayer2D;
    private int xImpulse = 0;
    private Control playAgainMenu;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        PlayerRigidBody2D = GetTree().Root.GetNode<RigidBody2D>("MainMenu/Game/Level01/RigidBody2D");
        _characterBody2D = GetTree().Root.GetNode<CharacterBody2D>("MainMenu/Game/Level01/RigidBody2D/Player");
        Player = GetTree().Root.GetNode<Node2D>("MainMenu/Game/Level01/RigidBody2D/Player");
        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        audioStreamPlayer2D = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        playAgainMenu = GetTree().Root.GetNode<Control>("MainMenu/PlayAgainMenu");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        

        // GD.Print("parent : " + GetParent().Name);
        PathFollow2D path = (PathFollow2D)GetParent();
        if(!GameManager.GlobalGameManager.pig_found)
        {
            path.Progress += speed * (float)delta;
        } 


        if(path.ProgressRatio > .5f)
        {
            xImpulse = -600;
            this.Scale = new Vector2(-1, 1);
        }
        else
        {
            xImpulse = 600;
            this.Scale = new Vector2(1, 1);
        }
	}

    public void _on_area_2d_body_entered(Node2D body)
    {
        if(body is player && Player.Visible)
        {
            GD.Print("PIG FOUND");
            GameManager.GlobalGameManager.pig_found = true;
            _animatedSprite2D.Play("king_attack");
            audioStreamPlayer2D.Play();
            Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("dead");
            GD.Print("Player animation: " + Player.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation);
            PlayerRigidBody2D.Sleeping = false;
            PlayerRigidBody2D.LinearVelocity = new Vector2(xImpulse, -1000);
            _characterBody2D.CollisionLayer = 2;
            _characterBody2D.CollisionMask = 2;
            GetNode<Timer>("Timer").Start();
        }
    }

    public void _on_timer_timeout()
    {
        playAgainMenu.Visible = true;
    }
}
