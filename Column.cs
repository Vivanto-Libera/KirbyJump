using Godot;
using System;

public partial class Column : Node2D
{
	public void setSpace(int space) 
	{
		for(int i = space; i < space + 3; i++) 
		{
			string brickName = "Brick" + i.ToString();
			GetNode<StaticBody2D>(brickName).QueueFree();
		}
	}
	public override void _Ready()
	{
		setSpace(0);
	}
}
