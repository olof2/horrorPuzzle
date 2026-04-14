using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class SanityMeterUI : VisualElement
{

    [SerializeField, DontCreateProperty]
    public float sanityLevel;

    [UxmlAttribute, CreateProperty]
    public float SanityLevel
    {
        get => sanityLevel;
        set
        {
            sanityLevel = Mathf.Clamp(value, 0.01f, 600f);
            MarkDirtyRepaint();
        }
    }
    public SanityMeterUI()
    {
        generateVisualContent += GenerateUI;
    }

    public void GenerateUI(MeshGenerationContext context)
    {
        //float width = 1920;
        //float height = 1080;

        //// Create the background rectangle
        //var backgroundRect = new Rect(0, 0, width, height);
        var painter = context.painter2D;
        //painter.BeginPath();
        //painter.lineWidth = 2f;
        //painter.Arc(new Vector2(width * 0.05f , height), width * 0.05f, 180f, 0f);
        //painter.ClosePath();
        //painter.fillColor = Color.white;
        //painter.Fill(FillRule.NonZero);
        //painter.Stroke();

        ////Fill
        //painter.BeginPath();
        //painter.LineTo(new Vector2(width * 0.05f, height));
        //painter.lineWidth = 2f;

        //float amount = 180f * ((100f - sanityLevel) / 100f);

        //painter.Arc(new Vector2(width * 0.05f, height), width * 0.05f, 180f, 0f - amount);
        //painter.ClosePath();
        //painter.fillColor = Color.red;
        //painter.Fill(FillRule.NonZero);
        //painter.Stroke();

        float x = 40;
        float y = 40;
        float width = 200;
        float height = 20;

        // Background
        painter.BeginPath();
        painter.MoveTo(new Vector2(x, y));
        painter.LineTo(new Vector2(x + width, y));
        painter.LineTo(new Vector2(x + width, y + height));
        painter.LineTo(new Vector2(x, y + height));
        painter.ClosePath();

        painter.fillColor = Color.white;
        painter.Fill();


        // Fill
        float amount = width * (sanityLevel / 600f);

        painter.BeginPath();
        painter.MoveTo(new Vector2(x, y));
        painter.LineTo(new Vector2(x + amount, y));
        painter.LineTo(new Vector2(x + amount, y + height));
        painter.LineTo(new Vector2(x, y + height));
        painter.ClosePath();

        painter.fillColor = Color.red;
        painter.Fill();

    }

    public void hideUI()
    {
        this.style.display = DisplayStyle.None;
        
    }
}
