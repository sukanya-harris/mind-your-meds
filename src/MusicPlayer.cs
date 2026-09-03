using Godot;
using System;

public partial class MusicPlayer : Node
{
	// Called when the node enters the scene tree for the first time.
	private AudioStreamPlayer player;
	public override void _Ready()
    {
        player = new AudioStreamPlayer();
		AddChild(player);

		player.Stream = GD.Load<AudioStream>("res://audio/a_night_of_dizzy_spells.mp3");
		player.VolumeDb = -10f;
		player.Play();
    }

	public void SetVolume(float db)
    {
        player.VolumeDb = db;
    }

	public void Stop()
    {
        player.Stop();
    }

	public void Play()
    {
        player.Play();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
