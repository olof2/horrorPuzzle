using UnityEngine;
using UnityEngine.UIElements;

public class GameOverScript : MonoBehaviour
{
    private UIDocument gameoverDocument;
    private UIDocument hudSanityMeter;

    private Button exitButton;
    private Button restartButton;
    private PlayerCameraLook playerCameraLook;
    private PlayerMovement playerMovement;
    private SanityMeter sanityMeter;
    private MainMenyEvents mainMenyEvents;
    private PausedMenUScript pausedMenUScript;

    private void Awake()
    {
        gameoverDocument = GetComponent<UIDocument>();
        gameoverDocument.rootVisualElement.style.display = DisplayStyle.None;
        //hudSanityMeter = FindFirstObjectByType<UIDocument>();
    }

    private void OnEnable()
    {
        var root = gameoverDocument.rootVisualElement;
        exitButton = root.Q("ExitButton") as Button;
        restartButton = root.Q("RestartButton") as Button;

        exitButton.RegisterCallback<ClickEvent>(OnExitGameClick);
        restartButton.RegisterCallback<ClickEvent>(OnRestartGameClick);
    }

    private void OnExitGameClick(ClickEvent evt)
    { 
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void GameOver()
    {
        //SanityMeterUI sanityMeterUI = hudSanityMeter.rootVisualElement.Q<SanityMeterUI>("SanityMeterUI");
        //sanityMeterUI.style.display = DisplayStyle.None;

        gameoverDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerCameraLook = FindFirstObjectByType<PlayerCameraLook>();
        mainMenyEvents = FindFirstObjectByType<MainMenyEvents>();
        pausedMenUScript = FindFirstObjectByType<PausedMenUScript>();
        sanityMeter = FindFirstObjectByType<SanityMeter>();

        pausedMenUScript.enabled = false;
        mainMenyEvents.enabled = false;
        playerMovement.enabled = false;
        playerCameraLook.enabled = false;
        sanityMeter.enabled = false;

         



        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

    }

    private void OnRestartGameClick(ClickEvent evt)
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

     void Update()
    {
        sanityMeter = FindFirstObjectByType<SanityMeter>();
        if ( sanityMeter.sanityLevel >= 600f)
        {
            GameOver();
            Debug.Log("Game Over! Sanity is at maximum!");
        }

        Debug.Log("Update körs");
    }


}

