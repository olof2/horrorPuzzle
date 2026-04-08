using UnityEngine;
using UnityEngine.UIElements;

public class GameOverScript : MonoBehaviour
{
    private UIDocument document;

    private Button exitButton;
    private Button restartButton;
    private PlayerCameraLook playerCameraLook;
    private PlayerMovement playerMovement;
    private SanityMeter sanityMeter;
    private MainMenyEvents mainMenyEvents;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        document.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void OnEnable()
    {
        var root = document.rootVisualElement;
        exitButton = root.Q("ExitButton") as Button;
        restartButton = root.Q("RestartButton") as Button;
        exitButton.RegisterCallback<ClickEvent>(OnExitGameClick);
       // restartButton.RegisterCallback<ClickEvent>(OnRestartGameClick);
    }

    private void OnExitGameClick(ClickEvent evt)
    { 
        UnityEngine.Application.Quit();
    }

    public void GameOver()
    {
        

        document.rootVisualElement.style.display = DisplayStyle.Flex;
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerCameraLook = FindFirstObjectByType<PlayerCameraLook>();
        mainMenyEvents = FindFirstObjectByType<MainMenyEvents>();
        mainMenyEvents.enabled = false;
        playerMovement.enabled = false;
        playerCameraLook.enabled = false;
        sanityMeter = FindFirstObjectByType<SanityMeter>();
        sanityMeter.enabled = false;

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

    }

     void Update()
    {
        sanityMeter = FindFirstObjectByType<SanityMeter>();
        if ( sanityMeter.sanityLevel >= 100f)
        {
            GameOver();
            Debug.Log("Game Over! Sanity is at maximum!");
        }

        Debug.Log("Update körs");
    }


}

