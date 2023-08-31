using Godot;
using System;

public partial class player : CharacterBody2D
{
	public const float Speed = 100.0f;
	public const float JumpVelocity = -200.0f;
    public bool animation_locked = false;
    public bool was_in_air = false;
    public Vector2 velocity;
    public Vector2 direction = Vector2.Zero;
    public AnimatedSprite2D _animationPlayer;
    public CollisionShape2D _collisionShape2D;


	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();


    public override void _Ready()
    {
        GameManager.Player = this;
        _animationPlayer = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
        //_animationPlayer.AnimationFinished += () => testMethod();
    }

	public override void _PhysicsProcess(double delta)
	{
		velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
            // GD.Print("not on floor");
			velocity.Y += gravity * (float)delta;
            was_in_air = true;
            if(was_in_air)
            {
                land();
            }
            
            

		// Handle Jump.
		if (Input.IsActionJustPressed("up") && IsOnFloor() && !GameManager.GlobalGameManager.pig_found && !GameManager.GlobalGameManager.level_complete)
			jump();

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		if(this.Visible && !GameManager.GlobalGameManager.pig_found && !GameManager.GlobalGameManager.level_complete)
        {
            direction = Input.GetVector("left", "right", "ui_up", "ui_down");
        }
        else
        {
            direction = Vector2.Zero;
        }

        if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
        update_animation();
        update_facing_direction();
	}

    public void update_animation()
    {
        if(!animation_locked && !GameManager.GlobalGameManager.pig_found)
        {
            if(direction != Vector2.Zero)
            {
                _animationPlayer.Play("run");
            } 
            else
            {
                _animationPlayer.Play("idle");
            }
        }
    }

    public void update_facing_direction()
    {
        if(direction.X > 0)
        {
            _animationPlayer.FlipH = true;

            // Fix for better hitbox
            _collisionShape2D.Position = new Vector2(-2,2);
        } 
        else if(direction.X < 0)
        {
            _animationPlayer.FlipH = false;

            // Fix for better hitbox
            _collisionShape2D.Position = new Vector2(2,2);
        }
    }

    public void jump()
    {
        // GD.Print("jumped");
        velocity.Y = JumpVelocity;
        animation_locked = true;
        _animationPlayer.Play("jump_start");
        

    }

    public void land()
    {
        // GD.Print(velocity.Y);
        if(IsOnFloor())
        {
            // GD.Print("landed");
            // _animationPlayer.Play("jump_end");
            animation_locked = false;
            
        }
        was_in_air = false;
    }

    // public void _on_diamond_pickup_body_entered(){
    //     GD.Print("diamond pickup");
    // }
}
