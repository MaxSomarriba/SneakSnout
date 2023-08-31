using Godot;
using System;

public partial class GameManager : Node2D
{
    public bool GamePaused = false;
    [Export]
    public static player Player;
    public bool touching_box = false;
    public ulong box_id = 0;
    public int Score = 0;
    public static GameManager GlobalGameManager;
    public bool pig_found = false;
    public bool level_complete = false;

    public override void _Ready()
    {
        if(GlobalGameManager == null)
        {
            GlobalGameManager = this;
        }
        else
        {
            // GD.Print("There is already a GameManager in the scene. Deleting this one.");
            // QueueFree();
        }
    }

    public void AddScore(int amount)
    {
        GlobalGameManager.Score += amount;
    }
}