using Godot;
using System;
using System.Diagnostics;
using System.IO;

public partial class Menu : Control
{
	[Signal]
	delegate void ColorAddedEventHandler(Color color);
	
	[Signal]
	delegate void ImageLoadedEventHandler(Image image);

	[Signal]
	delegate void AccumulatedColorSwitchedEventHandler(bool enabled);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<BaseButton>("%AccumulatedColorSwitch").Toggled += EmitSignalAccumulatedColorSwitched;
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
		var image = Image.LoadFromFile(path);
		EmitSignalImageLoaded(image);
	}
	
	public void OnColorAddedClicked()
	{
		var colorPicker = GetNode<ColorPicker>("%ColorPicker");
		var color = colorPicker.Color;
		EmitSignalColorAdded(color);
	}
	
	public void OnAccumulatedColorSwitched(bool enabled)
	{
	}
}
