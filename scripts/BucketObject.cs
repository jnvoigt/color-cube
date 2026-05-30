using Godot;
using System;
using System.Diagnostics;
using ColorCharacteristics.scripts.Model;

public partial class BucketObject : StaticBody3D
{
    private ShaderMaterial _shaderMaterial;
    private ColorBucket _colorBucket;
    private bool _showAccumulatedColor;

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
        _colorBucket = colorBucket;
        Position = new Vector3(colorBucket.BaseColor.R * Factor, colorBucket.BaseColor.G * Factor, colorBucket.BaseColor.B * Factor) - Vector3.One * Factor /2;
        UpdateVisuals();
    }

    private Color GetDisplayedColor()
    {
        if (_colorBucket is null)
        {
            return Colors.White;
        }
        
        if (_showAccumulatedColor)
        {
            return _colorBucket.AverageColor;
        }
        
        return _colorBucket.BaseColor;
    }

    public void UpdateVisuals()
    {
        SetColor(GetDisplayedColor());
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        var label = GetNode<Label>("%bucket_tooltip/%Label");
        var count = _colorBucket?.Count;
        label.Text = count is null ? "" : count.ToString();
        var labelSprite = GetNode<Sprite3D>("CountLabel");

        var showLabel = _showAccumulatedColor && count is not null && count > 0;
        labelSprite.Visible = showLabel;
    }

    public void ShowAccumulatedColor(bool enabled)
    {
        _showAccumulatedColor = enabled;
        UpdateVisuals();
    }
}
