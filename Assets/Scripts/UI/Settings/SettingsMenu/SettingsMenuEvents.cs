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

    private VisualElement controls;


    public PausedMenUScript pausedMenuScript;
    public SanityMeter sanityMeter;
    public MainMenyEvents mainMenyEvents;
    public PlayerCameraLook playerCameraLook;
    public PlayerMovement playerMovement;
    public SliderUIManager volumeSlider;
    public SliderUIManager sfxSlider;
    public SettingsMenuEvents settingsMenuEvents;

    public enum Source
    {
        pausedMenu,
        mainMenu,
        None

    }

    public Source currentSource;

    //Denna metod öppnar inställningsmenyn och tar emot en parameter som indikerar varifrån den öppnades,
    //så att den kan hantera "Back" knappen på rätt sätt.
    public void Open(Source source)
    {
        currentSource = source;
    }

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
        controls = root.Q("Controls") as VisualElement;
        controls.style.display = DisplayStyle.None;


        GameplayButton.RegisterCallback<ClickEvent>(OnClickGamePlay);
        AudioButton.RegisterCallback<ClickEvent>(OnClickAudio);
        GraphicsButton.RegisterCallback<ClickEvent>(OnClickGraphics);
        ControlsButton.RegisterCallback<ClickEvent>(OnClickControls);
        BackButton.RegisterCallback<ClickEvent>(OnClickBack);
    }

    private void OnClickGamePlay(ClickEvent evt)
    {
        volumeSlider.HideUI();
        HideControls();


        Debug.Log("Gameplay Button Clicked");
    }
    private void OnClickAudio(ClickEvent evt)
    {
        HideControls();

        //volumeSlider blir en Child till settingsDocument så att den visas som en pop-up ovanpå settings menyn

        var volumeSliderDocument = volumeSlider.GetComponent<UIDocument>();
       //var sfxSliderDoc = sfxSlider.GetComponent<UIDocument>();

       var settingsRoot = settingsDocument.rootVisualElement;
       var volumeSliderRoot = volumeSliderDocument.rootVisualElement;
       //var sfxSliderRoot = sfxSliderDoc.rootVisualElement;
       settingsRoot.Add(volumeSliderRoot);
       //settingsRoot.Add(sfxSliderRoot);
        volumeSlider.ShowAllUI();
        //sfxSlider.ShowSfxSliderUI();

        Debug.Log("Audio Button Clicked");
    }
    private void OnClickGraphics(ClickEvent evt)
    {
        volumeSlider.HideUI();
        HideControls();
        Debug.Log("Graphics Button Clicked");
    }
    private void OnClickControls(ClickEvent evt)
    {
        volumeSlider.HideUI();
        ShowControls();

        
        Debug.Log("Controls Button Clicked");
    }
    private void OnClickBack(ClickEvent evt)
    {
        if (currentSource == Source.pausedMenu)
        {
            pausedMenuScript.Paused();
        }
        if (currentSource == Source.mainMenu)
            mainMenyEvents.OpenMainMenu();

      

        volumeSlider.HideUI();
        //sfxSlider.HideSfxUI();

        Debug.Log("Back Button Clicked");
    }

    private void ShowControls()
    {
        controls.style.display = DisplayStyle.Flex;
    }
    private void HideControls()
    {
        controls.style.display = DisplayStyle.None;
    }



}
