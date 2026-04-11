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
    private MainMenyEvents mainMenyEvents;
    private PlayerCameraLook playerCameraLook;
    private PlayerMovement playerMovement;
    private SanityMeter sanityMeter;
    private SettingsMenuEvents settingsMenuEvents;
    private InteractableHud interactableHud;
    



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
        continueButton.RegisterCallback<ClickEvent>(OnPlayGameClick);
        exitButton.RegisterCallback<ClickEvent>(OnExitGameClick);
        settingsButton.RegisterCallback<ClickEvent>(OnSettingsClick);
        Debug.Log("ContinueButton: " + continueButton);
        Debug.Log("ExitButton: " + exitButton);

    }


    private void OnPlayGameClick(ClickEvent clickEvent)
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

    void Paused()
    {
        // Enablear pausmenyn och disablea allt annat
        
        pausedDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        mainMenyEvents = FindAnyObjectByType<MainMenyEvents>();
        mainMenyEvents.enabled = false;
        playerCameraLook = FindAnyObjectByType<PlayerCameraLook>();
        playerCameraLook.enabled = false;
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        playerMovement.enabled = false;
        sanityMeter = FindAnyObjectByType<SanityMeter>();
        sanityMeter.enabled = false; 
        settingsMenuEvents = FindAnyObjectByType<SettingsMenuEvents>();
        if (settingsMenuEvents != null)
            settingsMenuEvents.enabled = false;
        interactableHud = FindAnyObjectByType<InteractableHud>();
        if (interactableHud != null)
            interactableHud.enabled = false;

        var interactableHudDocument = interactableHud.GetComponent<UIDocument>();
        interactableHudDocument.rootVisualElement.style.display = DisplayStyle.None;

        // Låser inte musen och gör den synlig så att det är möjligt att klicka på knapparna i pausmenyn
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;



    }
    void UnPaused()
    {
        // Disablear pausmenyn och enablear allt annat

        pausedDocument.rootVisualElement.style.display = DisplayStyle.None;
        

        mainMenyEvents = FindAnyObjectByType<MainMenyEvents>();
        mainMenyEvents.enabled = true;
        playerCameraLook = FindAnyObjectByType<PlayerCameraLook>();
        playerCameraLook.enabled = true;
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        playerMovement.enabled = true;
        sanityMeter = FindAnyObjectByType<SanityMeter>();
        if (sanityMeter != null)
            sanityMeter.enabled = true;
        settingsMenuEvents = FindAnyObjectByType<SettingsMenuEvents>();
        settingsMenuEvents.enabled = true;
        interactableHud = FindAnyObjectByType<InteractableHud>();
        if (interactableHud != null)
            interactableHud.enabled = true;

        
        // Låser musen och gör den osynlig så att det är möjligt att spela spelet
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }

    public void OnSettingsClick(ClickEvent clickEvent)
    {
        // Enablear inställningsmenyn och disablear allt annat
        settingsMenuEvents = FindAnyObjectByType<SettingsMenuEvents>();
        if (settingsMenuEvents != null)
        settingsMenuEvents.enabled = true;
        var settingsDocument = settingsMenuEvents.GetComponent<UIDocument>();
        var root = settingsDocument.rootVisualElement;
        if (root != null)
        root.style.display = DisplayStyle.Flex;

        //Disablera alla andra script så
        mainMenyEvents = FindAnyObjectByType<MainMenyEvents>();
        if (mainMenyEvents != null)
            mainMenyEvents.enabled = false;

        playerCameraLook = FindAnyObjectByType<PlayerCameraLook>();
        if (playerCameraLook != null)
            playerCameraLook.enabled = false;

        playerMovement = FindAnyObjectByType<PlayerMovement>();
        if (playerMovement != null)
        playerMovement.enabled = false;

        sanityMeter = FindAnyObjectByType<SanityMeter>();
        if (sanityMeter != null)
            sanityMeter.enabled = false;

        pausedDocument.rootVisualElement.style.display = DisplayStyle.None;
        


        UnityEngine.Cursor.lockState = CursorLockMode.None; 
        UnityEngine.Cursor.visible = true;

        Debug.Log("Tröck på Settings");
    }

    void OnExitGameClick(ClickEvent clickEvent)
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false; // För att stoppa spelet i editorn
        Debug.Log("Tröck på Exit");
    }







}
