using Godot;
using System;
using ColorCharacteristics.scripts.Model;

public partial class ColorCube : Node3D
{
	private InstancePlaceholder _bucketPlaceholder;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var r = new RandomNumberGenerator();
		_bucketPlaceholder = GetNode<InstancePlaceholder>("%bucket");


		var buckets = new ColorBucket[]
		{
			new(Colors.Black),
			new(Colors.Red),
			new(Colors.Orange),
			new(Colors.Blue),
			new(Colors.Lime),
			new(Colors.Cyan),
			new(Colors.Magenta),
			new(Colors.Yellow),
			new(Colors.White),
			new(Colors.LightGray),
			new(Colors.DimGray),
		};
		foreach (var colorBucket in buckets)
		{
			var bucket = _bucketPlaceholder.CreateInstance() as BucketObject;
			bucket.Factor = 2;
			bucket.SetColorBucket(colorBucket);
		}
		
		for (var i = 0; i < 10; i++)
		{

		}
	}
}
