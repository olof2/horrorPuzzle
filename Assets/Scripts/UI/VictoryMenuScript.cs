using Mono.Cecil.Cil;
using PadlockSystem;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class VictoryMenuScript : MonoBehaviour
{

    private UIDocument victoryMenuDoc;
    private VisualElement visualElement;
    public PlayerCameraLook playerCameraLook;
    public PlayerMovement playerMovement;

    private Button exitButton;
    private Button returnMainMenuButton;

    private PadlockController padlockController;
    private Door door;
    PadlockItem padlockItem;


    private void Awake()
    {
        victoryMenuDoc = GetComponent<UIDocument>(); // Hämtar UI Dokumentet
       
        door = FindAnyObjectByType<Door>(); // Hittar dörren i scenen
        padlockController = FindAnyObjectByType<PadlockController>(); // Hittar padlockController i scenen

    }

    private void OnEnable()
    {
        var root = victoryMenuDoc.rootVisualElement;
        visualElement = root.Q<VisualElement>("Container");
        visualElement.style.display = DisplayStyle.None; // gömmer UI i awake


        exitButton = root.Q<Button>("ExitGame");
        returnMainMenuButton = root.Q<Button>("Restart");

        
        returnMainMenuButton.RegisterCallback<ClickEvent>(ReturnToMainMenu);
        exitButton.RegisterCallback<ClickEvent>(CloseGame);



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReturnToMainMenu(ClickEvent evt)
    {
        MainMenyEvents mainMenu = FindAnyObjectByType<MainMenyEvents>();
        mainMenu.OpenMainMenu();
    }

    void CloseGame(ClickEvent evt)
    {
        Application.Quit();
    }
    public void ShowUI()
    {

        //if (visualElement != null)
        //    visualElement.style.display = DisplayStyle.Flex; // Sätter UI synlig
        Transition();

        playerCameraLook.enabled = false;
        playerMovement.enabled = false;

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }

    public void HideUI()
    {
        if (visualElement != null)
            visualElement.style.display = DisplayStyle.None; // Gömmer UI
    }

    public void Transition()
    {
        var root = victoryMenuDoc.rootVisualElement;
        root.style.display = DisplayStyle.Flex;
        visualElement.style.display = DisplayStyle.Flex;

        root.style.opacity = 0;

        root.experimental.animation.Start(new StyleValues { opacity = 1 }, 4000).OnCompleted(() =>
        {
                root.style.display = DisplayStyle.Flex;
        });

    }

}
