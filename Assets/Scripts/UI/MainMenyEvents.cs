using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
public class MainMenyEvents : MonoBehaviour
{
    private UIDocument document;
    public UIDocument hudSanityMeter;
   
    private Button button;
    private Button settingsButton;
    public PlayerMovement playerMovement;
    public PlayerCameraLook playerCameraLook;
    public PausedMenUScript pausedMenUScript;
    public GameOverScript gameOverScript;
    public SettingsMenuEvents settingsMenuEvents;
    public MusicSystem musicSystem;
    public InteractableHud interactableHud;
    public VolumeSlider volumeSlider;

    public SanityMeter sanityMeter;
   
    

    private List<Button> menuButtons = new List<Button>();
    private List<Button> pauseMenuButtons = new List<Button>();

    private void Awake()
    {
        //Hittar dokumentet och knapparna i main meny, disablar spelkontrollerna och sanity meter.
        document = GetComponent<UIDocument>();
        
        //Stänker av spelkontrollerna och sanity meter så att de inte kan användas i main meny.
        //playerMovement = FindFirstObjectByType<PlayerMovement>();
        //playerCameraLook = FindFirstObjectByType<PlayerCameraLook>();

        var sanityMeterElement = hudSanityMeter.rootVisualElement.Q<VisualElement>("SanityMeterUI");
        sanityMeterElement.style.display = DisplayStyle.None;
       
        //var interactableHudElement = hudSanityMeter.rootVisualElement.Q<VisualElement>("E");
        //interactableHudElement.style.display = DisplayStyle.None;

        playerMovement.enabled = false;
        playerCameraLook.enabled = false;
        


        //sanityMeter = FindFirstObjectByType<SanityMeter>();
        sanityMeter.enabled = false;

       // pausedMenUScript = FindAnyObjectByType<PausedMenUScript>();
        if (pausedMenUScript != null)
            pausedMenUScript.enabled = false;
        //gameOverScript = FindAnyObjectByType<GameOverScript>();
        if (gameOverScript != null)
            gameOverScript.enabled = false;
        //settingsMenuEvents = FindAnyObjectByType<SettingsMenuEvents>();
        //if (settingsMenuEvents != null)
        //    settingsMenuEvents.enabled = false;
       // interactableHud = FindAnyObjectByType<InteractableHud>();
        if (interactableHud != null)
         interactableHud.enabled = false;
        //volumeSlider = FindAnyObjectByType<VolumeSlider>();
        if (volumeSlider != null)
            volumeSlider.enabled = false;




        button = document.rootVisualElement.Q("StartGameButton") as Button;
        button.RegisterCallback < ClickEvent>(OnPlayGameClick);
        settingsButton = document.rootVisualElement.Q("SettingsButton") as Button;
        button.RegisterCallback<ClickEvent>(OnSettingsClick);


        //menuButtons = document.rootVisualElement.Query<Button>().ToList();
        //for (int i = 0; i < menuButtons.Count; i++)
        //{
        //    menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        //}



    }


    private void OnDisable()
    {
        if(button  != null)
        button.UnregisterCallback < ClickEvent>(OnPlayGameClick);
        
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnPlayGameClick(ClickEvent clickEvent)
    {
        Debug.Log("You pressed the Start Button");

        //Enables spelkontrollerna och sanity meter efter man har startat spelet, disablar main meny dokumentet så att det inte syns längre.
        //playerMovement = FindFirstObjectByType<PlayerMovement>();
        //playerCameraLook = FindFirstObjectByType<PlayerCameraLook>();
        //settingsMenuEvents = FindFirstObjectByType<SettingsMenuEvents>();
        gameOverScript.enabled = true;
        playerMovement.enabled = true;
        playerCameraLook.enabled = true;
        pausedMenUScript.enabled = true;
        if (settingsMenuEvents != null)
            settingsMenuEvents.enabled = true;
        interactableHud.enabled = true;
        if (volumeSlider != null)
            volumeSlider.enabled = true;

        //sanityMeter = FindFirstObjectByType<SanityMeter>();
        //Enables sanity meter så att den kan användas i spelet.
        if (sanityMeter != null)
            sanityMeter.enabled = true;
        //Enable sanity metern UI
        SanityMeterUI sanityMeterUI = hudSanityMeter.rootVisualElement.Q<SanityMeterUI>("SanityMeterUI");
        sanityMeterUI.style.display = DisplayStyle.Flex;
        //Disablar main meny dokumentet så att det inte syns längre.
        if (document != null)
            document.enabled = false;

        if (MusicSystem.Instance != null)
            MusicSystem.Instance.Play("Test");



    }

    private void OnSettingsClick(ClickEvent clickEvent)
    {
       // pausedMenUScript = FindFirstObjectByType<PausedMenUScript>();
        pausedMenUScript.OnSettingsClick(clickEvent);
        Debug.Log("You pressed the Settings Button");

    }

    private void OnAllButtonsClick(ClickEvent clickEvent)
    {
        Debug.Log("You pressed the Start Button");
        
    }

    private void Update()
    {
        

    }
}
