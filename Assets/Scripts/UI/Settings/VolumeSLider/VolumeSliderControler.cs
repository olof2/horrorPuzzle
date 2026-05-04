using UnityEngine;
using UnityEngine.UIElements;

public class VolumeSliderControler : MonoBehaviour
{
    [SerializeField]
    private MusicSystem music;
    //public UIDocument musicSliderDoc;
    //public UIDocument sfxSliderDoc;
    //public VisualElement sfxSlider;
    //public VisualElement musicSlider;


    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.dataSource = music;

        var slider = root.Q<Slider>("VolumSlider");
        slider.value = music.Volume;
        slider.RegisterValueChangedCallback(evt => music.Volume = evt.newValue);

        //var sxfRoot = GetComponent<UIDocument>().rootVisualElement;
        //var sfxSlider = sxfRoot.Q<Slider>("SfxSlider");
    }
}
