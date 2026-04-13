using UnityEngine;
using UnityEngine.UIElements;

public class VolumeSliderControler : MonoBehaviour
{
    [SerializeField]
    private MusicSystem music;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.dataSource = music;

        var slider = root.Q<Slider>("VolumSlider");
        slider.value = music.Volume;
        slider.RegisterValueChangedCallback(evt => music.Volume = evt.newValue);
    }
}
