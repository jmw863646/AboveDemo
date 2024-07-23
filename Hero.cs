using Godot;
using System;

/// <summary>
/// A class representing the hero. 
/// </summary>
public partial class Hero : CharacterBody2D
{
    public const float SPEED = 400.0f;
    // Whether the hero is allowed to move
    public bool _moving = true;

    /// <summary>
    /// Get any input keys being used and update hero's velocity.
    /// </summary>
    private void get_input()
    {
        var input_direction = Input.GetVector("left", "right", "up", "down");

        Velocity = input_direction * SPEED;
    }

    /// <summary>
    /// Override the physics of the hero. The hero moves with the velocity determind by the arrow keys.
    /// </summary>
    /// <param name="delta">Unused.</param>
    public override void _PhysicsProcess(double delta)
    {
        get_input();

        // Only move if the dialog isn't open
        if (_moving)
        {
            MoveAndSlide();
        }
    }

    /// <summary>
    /// The hero has collided with another node on the screen. 
    /// </summary>
    /// <param name="other"></param>
    private void _on_area_2d_body_entered(Node2D other)
    {
        // Only care about the villain
        if ((other != null) && (other.Name == "Villain"))
        {
            // Stop moving
            _moving = false;

            // Simple popup to greet the villain
            var dialog = new AcceptDialog();
            dialog.DialogText = "So we meet again!";
            dialog.Title = "Greetings";
            dialog.Confirmed += () =>
            {
                // Remove the dialog
                dialog.QueueFree();

                // We can move again
                _moving = true;
            };
            AddChild(dialog);
            dialog.PopupCentered();
        }
    }
}
