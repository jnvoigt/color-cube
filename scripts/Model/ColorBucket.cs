using Godot;

namespace ColorCharacteristics.scripts.Model;

public class ColorBucket(Color baseColor)
{
    public Color BaseColor => baseColor;
    
    private double _sumR = 0;
    private double _sumG = 0;
    private double _sumB = 0;
    
    public int Count { get; private set; }
    
    public void Add(Color color)
    {
        _sumR += color.R;
        _sumG += color.G;
        _sumB += color.B;
        Count++;
    }
    
    public Color AverageColor => new Color((float)(_sumR / Count), (float)(_sumG / Count), (float)(_sumB / Count));
}