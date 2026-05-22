using UnityEngine;
using UnityEngine.UIElements;

public class SanityMeterUI : VisualElement
{
    public new class UxmlFactory : UxmlFactory<SanityMeterUI, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        private readonly UxmlFloatAttributeDescription sanityLevelAttribute =
            new UxmlFloatAttributeDescription
            {
                name = "sanity-level",
                defaultValue = 0f
            };

        private readonly UxmlFloatAttributeDescription maxSanityLevelAttribute =
            new UxmlFloatAttributeDescription
            {
                name = "max-sanity-level",
                defaultValue = 100f
            };

        public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
        {
            base.Init(visualElement, bag, context);

            SanityMeterUI meter = (SanityMeterUI)visualElement;
            meter.MaxSanityLevel = maxSanityLevelAttribute.GetValueFromBag(bag, context);
            meter.SanityLevel = sanityLevelAttribute.GetValueFromBag(bag, context);
        }
    }

    private float sanityLevel;
    private float maxSanityLevel = 100f;

    public float SanityLevel
    {
        get => sanityLevel;
        set
        {
            sanityLevel = Mathf.Clamp(value, 0f, maxSanityLevel);
            MarkDirtyRepaint();
        }
    }

    public float MaxSanityLevel
    {
        get => maxSanityLevel;
        set
        {
            maxSanityLevel = Mathf.Max(0.0001f, value);
            sanityLevel = Mathf.Clamp(sanityLevel, 0f, maxSanityLevel);
            MarkDirtyRepaint();
        }
    }

    public SanityMeterUI()
    {
        generateVisualContent += GenerateUI;
    }

    private void GenerateUI(MeshGenerationContext context)
    {
        Painter2D painter = context.painter2D;

        float x = 40f;
        float y = 40f;
        float width = 200f;
        float height = 20f;

        painter.BeginPath();
        painter.MoveTo(new Vector2(x, y));
        painter.LineTo(new Vector2(x + width, y));
        painter.LineTo(new Vector2(x + width, y + height));
        painter.LineTo(new Vector2(x, y + height));
        painter.ClosePath();
        painter.fillColor = Color.white;
        painter.Fill();

        float amount = width * (sanityLevel / maxSanityLevel);

        painter.BeginPath();
        painter.MoveTo(new Vector2(x, y));
        painter.LineTo(new Vector2(x + amount, y));
        painter.LineTo(new Vector2(x + amount, y + height));
        painter.LineTo(new Vector2(x, y + height));
        painter.LineTo(new Vector2(x, y));
        painter.ClosePath();
        painter.fillColor = Color.red;
        painter.Fill();
    }

    public void HideUI()
    {
        style.display = DisplayStyle.None;
    }
}