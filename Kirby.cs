using Godot;
using System;

public partial class Kirby : RigidBody2D
{
	[Export]
	public float jumpForce = 300f;
	[Signal]
	public delegate void HitEventHandler();
	public void Start(Vector2 position)
	{
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
		Position = position;
		Freeze = false;
		Show();
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Jump") && Freeze == false)
		{
			ApplyImpulse(Vector2.Up * jumpForce);
		}
	}
	public void _on_body_entered(Node body) 
	{
		Hide();
		SetDeferred(RigidBody2D.PropertyName.Freeze, true);
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
		EmitSignal(SignalName.Hit);
	}
}
