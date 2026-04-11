using UnityEngine;
using UnityEngine.UIElements;

public class VolumeSlider : MonoBehaviour
{
    private UIDocument volumeSliderDocument;

    private void Awake()
    {
        volumeSliderDocument = GetComponent<UIDocument>();
        volumeSliderDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        var root = volumeSliderDocument.rootVisualElement;
        var slider = root.Q<Slider>("VolumeSlider");
        slider.RegisterValueChangedCallback(OnVolumeSliderChanged);
    }

    private void OnVolumeSliderChanged(ChangeEvent<float> evt)
    {
        
        // Handle volume change here
    }
}
