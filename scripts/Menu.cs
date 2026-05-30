using Godot;
using System;
using System.Diagnostics;
using System.IO;

public partial class Menu : Control
{
	[Signal]
	delegate void ColorAddedEventHandler(Color color);
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnLoadButtonClicked()
	{
		var fileDialog = GetNode<FileDialog>("%FileDialog");
		fileDialog.Show();
	}

	public void OnFileSelected(string path)
	{
		var file = new FileInfo(path);
		
	}
	
	public void OnColorAddedClicked()
	{
		var colorPicker = GetNode<ColorPicker>("%ColorPicker");
		var color = colorPicker.Color;
		EmitSignalColorAdded(color);
	}
}
