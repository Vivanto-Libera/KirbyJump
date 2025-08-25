using Godot;
using System;
using System.Threading.Tasks;

public partial class Main : Node
{
	[Export]
	public PackedScene ColumnScene { get; set; }
	[Export]
	public float ColumnSpeed { get; set; } = 200f;
	[Signal]
	public delegate void StartEventHandler();

	private int score;
	private int lastSpace = 3;
	public async void GameStart()
	{
		GetNode<Label>("Message").Text = "Press Space to Start Game";
		lastSpace = 3;
		GetNode<Kirby>("Kirby").Freeze = true;
		setScore(0);
		GetNode<Label>("Message").Show();
		await waitForSpace();
		GetNode<Label>("Message").Hide();
		GetNode<Kirby>("Kirby").Start(GetNode<Marker2D>("StartPosition").Position);
		GetNode<Timer>("ColumnTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();
	}
	public async Task waitForSpace()
	{
		while (!Input.IsActionJustPressed("Jump"))
		{
			await ToSignal(GetTree().CreateTimer(0.016f), Timer.SignalName.Timeout);
		}
	}
	public void setScore(int newScore)
	{
		score = newScore;
		GetNode<Label>("Score").Text = score.ToString();

	}
	public void _on_column_timer_timeout() 
	{
		lastSpace = getSpace();
		Column column = ColumnScene.Instantiate<Column>();
		column.setSpace(lastSpace);
		column.Position = GetNode<Marker2D>("ColumnPosition").Position;
		column.LinearVelocity = new Vector2(-ColumnSpeed, 0);
		AddChild(column);
	}
	public void _on_score_timer_timeout() 
	{
		setScore(score + 1);
	}
	public async void _on_kirby_hit() 
	{
		GetNode<Timer>("ColumnTimer").Stop();
		GetNode<Timer>("ScoreTimer").Stop();
		GetNode<Label>("Message").Text = "You lose";
		GetNode<Label>("Message").Show();
		GetTree().CallGroup("Column", Node.MethodName.QueueFree);
		await ToSignal(GetTree().CreateTimer(3), Timer.SignalName.Timeout);
		EmitSignal(SignalName.Start);
	}

	public int getSpace() 
	{
		int min = lastSpace >= 2 ? lastSpace - 2 : 0;
		int max = lastSpace <= 4 ? lastSpace + 2 : 6;
		return GD.RandRange(min, max);
	}
	public override void _Ready()
	{
		GameStart();
	}
	public void _on_start() 
	{
		GameStart();
	}
}
