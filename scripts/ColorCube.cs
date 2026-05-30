using Godot;
using System;
using System.Linq;
using ColorCharacteristics.scripts.Model;

public partial class ColorCube : Node3D
{
	private InstancePlaceholder _bucketPlaceholder;
	private float _bucketFactor = 2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
			AddColorBucket(colorBucket);
		}
	}

	public void AddNewColor(Color color)
	{
		var bucket = new ColorBucket(color);
		AddColorBucket(bucket);
	}
	
	private void AddColorBucket(ColorBucket colorBucket)
	{
		var bucket = _bucketPlaceholder.CreateInstance() as BucketObject;
		bucket.Factor = _bucketFactor;
		bucket.SetColorBucket(colorBucket);
	}
}
