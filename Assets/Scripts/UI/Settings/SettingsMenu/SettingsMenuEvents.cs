using UnityEngine;
using UnityEngine.UIElements;

public class SettingsMenuEvents : MonoBehaviour
{
    private Button GameplayButton;
    private Button AudioButton;
    private Button GraphicsButton;
    private Button BackButton;
    private Button ControlsButton;

    public UIDocument settingsDocument;


    public PausedMenUScript pausedMenuScript;
    private SanityMeter sanityMeter;
    private MainMenyEvents mainMenyEvents;
    private PlayerCameraLook playerCameraLook;
    private PlayerMovement playerMovement;
    public VolumeSlider volumeSlider;



    public void Awake()
    {
        settingsDocument = GetComponent<UIDocument>();

        settingsDocument.rootVisualElement.style.display = DisplayStyle.None;
    }
    private void OnEnable()
    {
        var root = settingsDocument.rootVisualElement;
        GameplayButton = root.Q("GameplayButton") as Button;
        AudioButton = root.Q("AudioButton") as Button;
        GraphicsButton = root.Q("GraphicsButton") as Button;
        ControlsButton = root.Q("ControlsButton") as Button;
        BackButton = root.Q("BackButton") as Button;

        GameplayButton.RegisterCallback<ClickEvent>(OnClickGamePlay);
        AudioButton.RegisterCallback<ClickEvent>(OnClickAudio);
        GraphicsButton.RegisterCallback<ClickEvent>(OnClickGraphics);
        ControlsButton.RegisterCallback<ClickEvent>(OnClickControls);
        BackButton.RegisterCallback<ClickEvent>(OnClickBack);
    }

    private void OnClickGamePlay(ClickEvent evt)
    {

        Debug.Log("Gameplay Button Clicked");
    }
    private void OnClickAudio(ClickEvent evt)
    {
        //volumeSlider blir en Child till settingsDocument så att den visas som en pop-up ovanpå settings menyn
        
        var volumeSliderDocument = volumeSlider.GetComponent<UIDocument>();
        var settingsRoot = settingsDocument.rootVisualElement;
        var volumeSliderRoot = volumeSliderDocument.rootVisualElement;
        settingsRoot.Add(volumeSliderRoot);
        volumeSlider.ShowUI();

        Debug.Log("Audio Button Clicked");
    }
    private void OnClickGraphics(ClickEvent evt)
    {
        Debug.Log("Graphics Button Clicked");
    }
    private void OnClickControls(ClickEvent evt)
    {
        Debug.Log("Controls Button Clicked");
    }
    private void OnClickBack(ClickEvent evt)
    {
        pausedMenuScript.Paused();
        Debug.Log("Back Button Clicked");
    }



}
