using Godot;
using System;

public partial class Kirby : RigidBody2D
{
	[Export]
	public float jumpForce = 300f;

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Jump"))
		{
			ApplyImpulse(Vector2.Up * jumpForce);
		}
	}
}
