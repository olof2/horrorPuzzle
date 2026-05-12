using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class SliderUIManager : MonoBehaviour
{
    public  UIDocument volumeSliderDocument;
    public UIDocument sfxSliderDoc;
    private Slider sfxSlider;
    private Slider volumeSlider;
    private MusicSystem musicSystem;
    private float volume;

    public AudioMixer SFXmixer;

    private void Awake()
    {
        volumeSliderDocument = GetComponent<UIDocument>();
        volumeSliderDocument.rootVisualElement.style.display = DisplayStyle.None;

        sfxSliderDoc = GetComponent<UIDocument>();
        sfxSliderDoc.rootVisualElement.style.display = DisplayStyle.None;

        //volumeSlider.value = MusicSystem.Instance.GetVolume();
        
    }

    private void OnEnable()
    {
        var root = volumeSliderDocument.rootVisualElement;
        var _root = volumeSliderDocument.rootVisualElement;

        volumeSlider = root.Q("VolumeSlider") as Slider;
        sfxSlider = _root.Q("SfxSlider") as Slider;


        //if (volumeSlider != null)
        //{
        //    volumeSlider.value = 1f;

        //}

        //var sftRoot = sfxSliderDoc.rootVisualElement;
        // sfxSlider = sftRoot.Q("SfxSlider") as Slider;
    }

    public void ShowUI()
    {
        // Positioner för volymreglaget så att det visas på rätt plats i förhållande till inställningsmenyn.
        var root = volumeSliderDocument.rootVisualElement;
        root.style.position = Position.Absolute;
        root.style.top = 400;
        root.style.left = 300;
        root.style.right = 0;
        root.style.bottom = 0;

        root.style.display = DisplayStyle.Flex;

        var _root = volumeSliderDocument.rootVisualElement;
        _root.style.position = Position.Absolute;
        _root.style.top = 400;
        _root.style.left = 300;
        _root.style.right = 0;
        _root.style.bottom = 0;

        _root.style.display = DisplayStyle.Flex;

    }

    public void ShowSfxSliderUI()
    {
        var root = sfxSliderDoc.rootVisualElement;
        root.style.position = Position.Absolute;
        root.style.top = 300;
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

        // Uppdatera SFX-volymen i AudioMixern baserat på sfxSlider-värdet
        if(sfxSlider != null && SFXmixer != null)
        {
            //float sfxVolume = sfxSlider.value;
            float sfxVolume = Mathf.Clamp(sfxSlider.value, 0.0001f, 1f); // Förhindra log10(0) genom att sätta en minimal volym     
            SFXmixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20); // Omvandla till decibel

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
    public void HideSfxUI()
    {
        sfxSliderDoc.rootVisualElement.style.display = DisplayStyle.None;
    }
}
