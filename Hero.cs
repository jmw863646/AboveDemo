using Godot;
using System;

public partial class Hero : CharacterBody2D
{
	public const float SPEED = 400.0f;

	private void get_input()
	{
		var input_direction = Input.GetVector("left", "right", "up", "down");

		Velocity = input_direction * SPEED;
	}

    public override void _Ready()
    {
        base._Ready();

		Position = new Vector2(200, 200);
    }

    public override void _PhysicsProcess(double delta)
	{
		get_input();
		MoveAndSlide();
	}
}
