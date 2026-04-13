using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class MainMenyEvents : MonoBehaviour
{
    private UIDocument document;
    public UIDocument hudSanityMeter;
    private UIDocument settingsDoc;
   
    private Button startButton;
    private Button settingsButton;
    private Button exitButton;
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
        
        var sanityMeterElement = hudSanityMeter.rootVisualElement.Q<VisualElement>("SanityMeterUI");
        sanityMeterElement.style.display = DisplayStyle.None;

        playerMovement.enabled = false;
        playerCameraLook.enabled = false;
        sanityMeter.enabled = false;

        if (pausedMenUScript != null)
            pausedMenUScript.enabled = false;
        if (gameOverScript != null)
            gameOverScript.enabled = false;
        if (settingsMenuEvents != null) settingsMenuEvents.enabled = false;
        
        if (interactableHud != null)
         interactableHud.enabled = false;
        if (volumeSlider != null)
            volumeSlider.enabled = false;

        var root = document.rootVisualElement;

        startButton = root.Q<Button>("StartGameButton");
        settingsButton = root.Q<Button>("Settings");
        exitButton = root.Q<Button>("Exit");
        
      


    }

    private void OnEnable()
    {
        var root = document.rootVisualElement;

      

        startButton.RegisterCallback<ClickEvent>(OnPlayGameClick);
        settingsButton.RegisterCallback<ClickEvent>(OnSettingsClick);
        exitButton.RegisterCallback<ClickEvent>(OnExitClick);


    }



    private void OnDisable()
    {
        if(startButton  != null)
        startButton.UnregisterCallback < ClickEvent>(OnPlayGameClick);
        if (settingsButton != null)
            settingsButton.UnregisterCallback<ClickEvent>(OnSettingsClick);
        if (exitButton != null)
            exitButton.UnregisterCallback<ClickEvent>(OnExitClick);









        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnPlayGameClick(ClickEvent clickEvent)
    {
        Debug.Log("You pressed the Start Button");

       
      
        gameOverScript.enabled = true;
        playerMovement.enabled = true;
        playerCameraLook.enabled = true;
        if (pausedMenUScript  != null)
        pausedMenUScript.enabled = true;

        if (settingsMenuEvents != null)
            settingsMenuEvents.enabled = true;
        interactableHud.enabled = true;
        if (volumeSlider != null)
            volumeSlider.enabled = true;

       
        if (sanityMeter != null)
            sanityMeter.enabled = true;
        //Enable sanity metern UI
        SanityMeterUI sanityMeterUI = hudSanityMeter.rootVisualElement.Q<SanityMeterUI>("SanityMeterUI");
        sanityMeterUI.style.display = DisplayStyle.Flex;


        //if (document != null)
        //    document.enabled = false;
            document.rootVisualElement.style.display = DisplayStyle.None;
    
           

        if (MusicSystem.Instance != null)
            MusicSystem.Instance.Play("Test");



    }

   public void OpenMainMenu()
    {
        var root = document.rootVisualElement;
        root.style.display = DisplayStyle.Flex;

        //gameObject.SetActive(false);
        //gameObject.SetActive(true);


        var sanityMeterElement = hudSanityMeter.rootVisualElement.Q<VisualElement>("SanityMeterUI");
        sanityMeterElement.style.display = DisplayStyle.None;

        var settingsDocument = settingsMenuEvents.GetComponent<UIDocument>();
        settingsDocument.rootVisualElement.style.display = DisplayStyle.None;

        playerMovement.enabled = false;
        playerCameraLook.enabled = false;
        sanityMeter.enabled = false;

        if (pausedMenUScript != null)
            pausedMenUScript.enabled = false;
        if (gameOverScript != null)
            gameOverScript.enabled = false;
        if (settingsMenuEvents != null) 
            settingsMenuEvents.enabled = false;

        if (interactableHud != null)
            interactableHud.enabled = false;
        if (volumeSlider != null)
            volumeSlider.enabled = false;
 

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

    }

    private void OnSettingsClick(ClickEvent clickEvent)
    {
        // Enablear inställningsmenyn och disablear allt annat
        settingsMenuEvents.Open(SettingsMenuEvents.Source.mainMenu);

        var settingsDocument = settingsMenuEvents.GetComponent<UIDocument>();
       // settingsDocument.enabled = true;
        var root = settingsDocument.rootVisualElement;
        if (root != null)
            root.style.display = DisplayStyle.Flex;

        document.rootVisualElement.style.display = DisplayStyle.None;
      // document.enabled = false;
        

        if (playerCameraLook != null)
            playerCameraLook.enabled = false;

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (sanityMeter != null)
            sanityMeter.enabled = false;

        




        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;


    }


    private void OnAllButtonsClick(ClickEvent clickEvent)
    {
        Debug.Log("You pressed the Start Button");
        
    }

    private void OnExitClick(ClickEvent clickEvent)
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void Update()
    {
        

    }
}
