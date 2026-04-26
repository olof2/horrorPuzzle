using System.Xml.Linq;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.UIElements;

public class PausedMenUScript : MonoBehaviour
{
    private UIDocument pausedDocument;
    public UIDocument hudSanityMeter;
    private Button continueButton;
    private Button exitButton;
    private Button settingsButton;
    public MainMenyEvents mainMenyEvents;
    public PlayerCameraLook playerCameraLook;
    public PlayerMovement playerMovement;
    public SanityMeter sanityMeter;
    public SettingsMenuEvents settingsMenuEvents;
    public InteractableHud interactableHud;
    public SanityMeterUI sanityMeterUI;
    public MusicSystem musicSystem;



    private void Awake()
    {
        pausedDocument = GetComponent<UIDocument>();

        pausedDocument.rootVisualElement.style.display = DisplayStyle.None;
      
    }

    private void OnEnable()
    {
        //Hittar knapparna i pausmenyn varje gång scriptet enableas.
        var root = pausedDocument.rootVisualElement;
        continueButton = root.Q("ContinueButton") as Button;
        exitButton = root.Q("ExitButton") as Button;
        settingsButton = root.Q("SettingsButton") as Button;

        //Regristerar callbacks för knapparna i pausmenyn, UnPaused() och OnExitGameClick() metoderna kommer att köras när knapparna klickas på.
        continueButton.RegisterCallback<ClickEvent>(OnContinueClick);
        exitButton.RegisterCallback<ClickEvent>(OnExitGameClick);
        settingsButton.RegisterCallback<ClickEvent>(OnSettingsClick);
        Debug.Log("ContinueButton: " + continueButton);
        Debug.Log("ExitButton: " + exitButton);

    }


    private void OnContinueClick(ClickEvent clickEvent)
    {
        UnPaused();

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Paused();
            Debug.Log("Tröck på P");
        }

    }

    public void Paused()
    {
        //
        interactableHud.isPaused = true;

        //Visa pausmenyn UI och gömmer setttings UI
        var settingsDocument = settingsMenuEvents.GetComponent<UIDocument>();
        settingsDocument.rootVisualElement.style.display = DisplayStyle.None;
        pausedDocument.rootVisualElement.style.display = DisplayStyle.Flex;

        //Disablear alla andra script så att spelaren inte kan röra sig eller titta runt när pausmenyn är uppe,
        //och gömmer sanity metern och diaktiverar mätareninteraktions-HUD:en.
        mainMenyEvents.enabled = false;
        playerCameraLook.enabled = false;
        playerMovement.enabled = false;
        sanityMeter.enabled = false;
        if (settingsMenuEvents != null)
        settingsMenuEvents.enabled = false;

        var sanityMeterElement = hudSanityMeter.rootVisualElement.Q<VisualElement>("SanityMeterUI");
        sanityMeterElement.style.display = DisplayStyle.None;
         
        var interactableHudDocument = interactableHud.GetComponent<UIDocument>();
        interactableHudDocument.rootVisualElement.style.display = DisplayStyle.None;

        MusicSystem.Instance.Pause("Test");

        // Låser inte musen och gör den synlig så att det är möjligt att klicka på knapparna i pausmenyn
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;



        }
    
    public void UnPaused()
    {
        // Disablear pausmenyn och enablear allt annat
         interactableHud.isPaused = false;
        pausedDocument.rootVisualElement.style.display = DisplayStyle.None;
        

        mainMenyEvents.enabled = true;
        playerCameraLook.enabled = true;
        playerMovement.enabled = true;
        if (sanityMeter != null)
            sanityMeter.enabled = true;
        settingsMenuEvents.enabled = true;
       
        var interactableHudDocument = interactableHud.GetComponent<UIDocument>();
        interactableHudDocument.rootVisualElement.style.display = DisplayStyle.Flex;

        var sanityMeterElement = hudSanityMeter.rootVisualElement.Q<VisualElement>("SanityMeterUI");
        sanityMeterElement.style.display = DisplayStyle.Flex;

        MusicSystem.Instance.Play("Test");

        // Låser musen och gör den osynlig så att det är möjligt att spela spelet
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    public void OnSettingsClick(ClickEvent clickEvent)
    {

        // Talar om att settings öppnades från pausmenyn.
        settingsMenuEvents.Open(SettingsMenuEvents.Source.pausedMenu);

        var settingsDocument = settingsMenuEvents.GetComponent<UIDocument>();
        var root = settingsDocument.rootVisualElement;
        if (root != null)
            root.style.display = DisplayStyle.Flex;

        

        //Disablera alla andra script så
        if (mainMenyEvents != null)
            mainMenyEvents.enabled = false;

        if (playerCameraLook != null)
            playerCameraLook.enabled = false;

        if (playerMovement != null)
        playerMovement.enabled = false;

        if (sanityMeter != null)
            sanityMeter.enabled = false;

        pausedDocument.rootVisualElement.style.display = DisplayStyle.None;

        var sanityMeterElement = hudSanityMeter.rootVisualElement.Q<VisualElement>("SanityMeterUI");
        sanityMeterElement.style.display = DisplayStyle.None;


        UnityEngine.Cursor.lockState = CursorLockMode.None; 
        UnityEngine.Cursor.visible = true;

    }

    void OnExitGameClick(ClickEvent clickEvent)
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false; // För att stoppa spelet i editorn
        Debug.Log("Tröck på Exit");
    }







}
