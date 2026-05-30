using Godot;
using System;
using System.Diagnostics;
using ColorCharacteristics.scripts.Model;

public partial class BucketObject : StaticBody3D
{
    private ShaderMaterial _shaderMaterial;

    public override void _InputEvent(Camera3D camera, InputEvent @event, Vector3 eventPosition, Vector3 normal, int shapeIdx)
    {
        if (@event is InputEventMouseButton {Pressed: true, ButtonIndex: MouseButton.Left})
        {
            Debug.WriteLine($"bucket clicked {Name}");
        }
    }

    public override void _Ready()
    {
        var mesh = GetNode<MeshInstance3D>("%MeshInstance3D");
        var material = mesh.GetActiveMaterial(0);
        
        if (material is ShaderMaterial shaderMaterial)
        {
            var duplicate = shaderMaterial.Duplicate() as ShaderMaterial;
            _shaderMaterial = duplicate;
            mesh.SetMaterialOverride(duplicate);
        }
    }

    private void SetColor(Color color)
    {
        if (_shaderMaterial is not null)
        {
            _shaderMaterial.SetShaderParameter("color", color);
        }
    }
    
    public float Factor { get; set; }

    public void SetColorBucket(ColorBucket colorBucket)
    {
        Position = new Vector3(colorBucket.BaseColor.R * Factor, colorBucket.BaseColor.G * Factor, colorBucket.BaseColor.B * Factor) - Vector3.One * Factor /2;
        SetColor(colorBucket.BaseColor);
    }
}
