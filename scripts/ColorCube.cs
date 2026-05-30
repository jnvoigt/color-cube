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
            Colors.PaleGreen,
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

        GenerateCubeGrid(3);
    }

    private void GenerateCubeGrid(int lineCount)
    {
        var gridMesh = GetNode<MeshInstance3D>("%GridMesh");
        var mesh = new ImmediateMesh();
        gridMesh.Mesh = mesh;

        // generate grid into mesh, primitive lines

        GenerateLines(mesh, lineCount, Vector3.Back, Vector3.Right, Vector3.Up);
        GenerateLines(mesh, lineCount, Vector3.Right, Vector3.Up, Vector3.Back);
        GenerateLines(mesh, lineCount, Vector3.Up, Vector3.Back, Vector3.Right);
    }

    private void GenerateLines(ImmediateMesh mesh, int lineCount, Vector3 direction, Vector3 offset1, Vector3 offset2)
    {
        var initialPoint = -(direction + offset1 + offset2) * _bucketFactor / 2;
        var segmentLength = _bucketFactor / lineCount;
        
        for (var offset1Index = 0; offset1Index < lineCount + 1; offset1Index++)
        {
            for (var offset2Index = 0; offset2Index < lineCount + 1; offset2Index++)
            {
                for (var segment = 0; segment < lineCount; segment++)
                {
                    var directionShift = segment*segmentLength*direction;
                    var offset1Shift = offset1*segmentLength*offset1Index;
                    var offset2Shift = offset2*segmentLength*offset2Index;
                    
                    var start = initialPoint + directionShift + offset1Shift + offset2Shift;
                    var end = start + segmentLength*direction;

                    mesh.SurfaceBegin(Mesh.PrimitiveType.Lines);
                    mesh.SurfaceAddVertex(start);
                    mesh.SurfaceAddVertex(end);
                    mesh.SurfaceEnd();
                }
            }
            
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
        foreach (var bucketObject in GetBucketObjects())
        {
            bucketObject.UpdateVisuals();
        }
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
        var bucketObjects = GetBucketObjects();
        foreach (var bucket in bucketObjects)
        {
            bucket.ShowAccumulatedColor(enabled);
        }
    }

    private IEnumerable<BucketObject> GetBucketObjects() => GetChildren().OfType<BucketObject>();
}