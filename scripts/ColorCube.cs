using Godot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ColorCharacteristics.scripts.Model;

public partial class ColorCube : Node3D
{
    private InstancePlaceholder _bucketPlaceholder;
    private float _bucketFactor = 2;

    private List<ColorBucket> _buckets = [];
    private bool _showAccumulatedColor;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        var defaultColors = new Color[]
        {
            Colors.Black,
            Colors.Red,
            Colors.Maroon,
            Colors.Crimson,
            Colors.Orange,
            Colors.Gold,
            Colors.Yellow,
            Colors.YellowGreen,
            Colors.Lime,
            Colors.Teal,
            Colors.MediumSpringGreen,
            Colors.Cyan,
            Colors.DeepSkyBlue,
            Colors.Blue,
            Colors.DarkBlue,
            Colors.Purple,
            Colors.Magenta,
            Colors.Pink,
            Colors.SandyBrown,
            Colors.SaddleBrown,
            Colors.Brown,
            Colors.White,
            Colors.LightGray,
            Colors.DimGray,
        };
        _bucketPlaceholder = GetNode<InstancePlaceholder>("%bucket");
        foreach (var colorBucket in defaultColors)
        {
            CreateBucketFromColor(colorBucket);
        }
    }

    public void CreateBucketFromColor(Color color)
    {
        var bucket = new ColorBucket(color);
        _buckets.Add(bucket);
        CreateBucketObject(bucket);
    }

    private void CreateBucketObject(ColorBucket bucket)
    {
        var bucketInstance = _bucketPlaceholder.CreateInstance() as BucketObject;
        bucketInstance.Factor = _bucketFactor;
        bucketInstance.SetColorBucket(bucket);
        bucketInstance.ShowAccumulatedColor(_showAccumulatedColor);
        
    }

    public void FillBucketsWithImageData(Image image)
    {
        ResetBuckets();
        
        var width = image.GetWidth();
        var height = image.GetHeight();

        var counter = 0;
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var color = image.GetPixel(x, y);
                if (color.A > 0)
                {
                    counter++;
                    AddColorToBucket(color);
                }
            }
        }
        
        Debug.WriteLine($"Add {counter} colors to buckets");
    }

    private void ResetBuckets()
    {
        foreach (var bucket in _buckets)
        {
            bucket.Reset();
        }
    }

    private void AddColorToBucket(Color color)
    {
        // find closest bucket and add color
        var closestBucket = _buckets.OrderBy(b => b.DistanceSquaredTo(color)).First();
        closestBucket.Add(color);
    }
    
    public void ToggleBucketDisplayMode(bool enabled)
    {
        _showAccumulatedColor = enabled;
        var bucketObjects = GetChildren().OfType<BucketObject>();
        foreach (var bucket in bucketObjects)
        {
            bucket.ShowAccumulatedColor(enabled);
        }
    }
}