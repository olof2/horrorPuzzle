using UnityEngine;
using UnityEngine.UIElements;

public class VolumeSlider : MonoBehaviour
{
    private UIDocument volumeSliderDocument;
    private Slider volumeSlider;
    private MusicSystem musicSystem;
    private float volume;

    private void Awake()
    {
        volumeSliderDocument = GetComponent<UIDocument>();
        volumeSliderDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        var root = volumeSliderDocument.rootVisualElement;
        
        
        volumeSlider = root.Q("VolumeSlider") as Slider;

       
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
    public void HideUI()
    {
        volumeSliderDocument.rootVisualElement.style.display = DisplayStyle.None;
    }
}
