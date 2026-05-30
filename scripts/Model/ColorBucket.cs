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

    public Color AverageColor
    {
        get
        {
            if (Count == 0)
            {
                return Colors.Transparent;
            }
            
            return new((float)(_sumR / Count), (float)(_sumG / Count), (float)(_sumB / Count));
        }
    }

    public float DistanceSquaredTo(Color color)
    {
        var delta = new Vector3(
            BaseColor.R - color.R,
            BaseColor.G - color.G,
            BaseColor.B - color.B);

        return delta.LengthSquared();
    }

    public void Reset()
    {
        _sumR = 0;
        _sumG = 0;
        _sumB = 0;
        Count = 0;
    }
}