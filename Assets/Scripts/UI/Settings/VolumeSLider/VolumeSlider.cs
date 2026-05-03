using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class VolumeSlider : MonoBehaviour
{
    private UIDocument volumeSliderDocument;
    private Slider volumeSlider;
    private MusicSystem musicSystem;
    private float volume;

    public float CurrentSFXVolume { get; private set; } = 1f;

    public AudioMixer SFXmixer;

    private void Awake()
    {
        volumeSliderDocument = GetComponent<UIDocument>();
        volumeSliderDocument.rootVisualElement.style.display = DisplayStyle.None;

        //volumeSlider.value = MusicSystem.Instance.GetVolume();
        
    }

    private void SetVolume(float value)
    {
        CurrentSFXVolume = value;

        SFXmixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
        
    }

    private void OnEnable()
    {
        var root = volumeSliderDocument.rootVisualElement;
        
        volumeSlider = root.Q("VolumeSlider") as Slider;

        if (volumeSlider != null)
        {
            volumeSlider.value = 1f;

            volumeSlider.RegisterValueChangedCallback(evt => SetVolume(evt.newValue));
        }
    }

    public void ShowUI()
    {
        // Positioner för volymreglaget så att det visas på rätt plats i förhållande till inställningsmenyn.
        var root = volumeSliderDocument.rootVisualElement;
        root.style.position = Position.Absolute;
        root.style.top = 400;
        root.style.left = 100;
        root.style.right = 0;
        root.style.bottom = 0;

        root.style.display = DisplayStyle.Flex;

    }

    void Update()
    {
        if (volumeSlider != null)
        {
            volume = volumeSlider.value;
            // Här kan du implementera logiken för att uppdatera volymen i ditt ljud
        }
    }

    private void ChangeVolume()
    {
        
        
    }

    private void OnBeforeTransformParentChanged()
    {
        // När inställningsmenyn stängs, döljer vi volymreglaget
        HideUI();
    }

    public void HideUI()
    {
        volumeSliderDocument.rootVisualElement.style.display = DisplayStyle.None;
    }
}
