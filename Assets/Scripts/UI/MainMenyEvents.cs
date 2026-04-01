using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
public class MainMenyEvents : MonoBehaviour
{
    private UIDocument document;
    private Button button;
    private PlayerMovement playerMovement;
    private PlayerCameraLook playerCameraLook;

    private VisualTreeAsset startMenu;
    private VisualTreeAsset pausMenu;

    

    private List<Button> menuButtons = new List<Button>();

    private void Awake()
    {
        document = GetComponent<UIDocument>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerCameraLook = FindFirstObjectByType<PlayerCameraLook>();
        playerMovement.enabled = false;
        playerCameraLook.enabled = false;


        button = document.rootVisualElement.Q("StartGameButton") as Button;
        button.RegisterCallback < ClickEvent>(OnPlayGameClick);

        menuButtons = document.rootVisualElement.Query<Button>().ToList();
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].RegisterCallback<ClickEvent>(OnAllButtonsClick);
        }

        
    }


    private void OnDisable()
    {
        button.UnregisterCallback < ClickEvent>(OnPlayGameClick);
        for (int i = 0; i < menuButtons.Count; i++)
        {
            menuButtons[i].UnregisterCallback<ClickEvent>(OnAllButtonsClick);
        }
    }

    private void OnPlayGameClick(ClickEvent clickEvent)
    {
        Debug.Log("You pressed the Start Button");

        playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerCameraLook = FindFirstObjectByType<PlayerCameraLook>();
        playerMovement.enabled = true;
        playerCameraLook.enabled = true;

        document.enabled = false;






    }

    private void OnAllButtonsClick(ClickEvent clickEvent)
    {
        Debug.Log("You pressed the Start Button");
        
    }
}
