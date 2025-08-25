using Godot;
using System;
using System.Threading.Tasks;

public partial class Column : RigidBody2D
{
	public void setSpace(int space) 
	{
		for(int i = space; i < space + 2; i++) 
		{
			string brickName = "Brick" + i.ToString();
			GetNode<StaticBody2D>(brickName).QueueFree();
		}
	}
	public override void _Process(double delta)
	{
		if(Position.X == -64) 
		{
			QueueFree();
		}
	}
}
