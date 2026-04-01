using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
public class MainMenyEvents : MonoBehaviour
{
    private UIDocument document;
    private UIDocument sanityDocument;
    private Button button;
    private PlayerMovement playerMovement;
    private PlayerCameraLook playerCameraLook;

    private SanityMeter sanityMeter;
    private SanityMeterUI sanityMeterUI;

    private VisualTreeAsset SanityMeterUI;
    private VisualTreeAsset pausMenu;

    

    private List<Button> menuButtons = new List<Button>();

    private void Awake()
    {
        //Hittar dokumentet och knapparna i main meny, disablar spelkontrollerna och sanity meter.
        document = GetComponent<UIDocument>();
        sanityDocument = GetComponent<UIDocument>();
        
        //Stänker av spelkontrollerna och sanity meter så att de inte kan användas i main meny.
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerCameraLook = FindFirstObjectByType<PlayerCameraLook>();
        playerMovement.enabled = false;
        playerCameraLook.enabled = false;
        

        sanityMeter = FindFirstObjectByType<SanityMeter>();
        sanityMeter.enabled = false;

        


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

        //Enables spelkontrollerna och sanity meter efter man har startat spelet, disablar main meny dokumentet så att det inte syns längre.
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        playerCameraLook = FindFirstObjectByType<PlayerCameraLook>();
        playerMovement.enabled = true;
        playerCameraLook.enabled = true;

        sanityMeter = FindFirstObjectByType<SanityMeter>();
        //Enables sanity meter så att den kan användas i spelet.
        sanityMeter.enabled = true;
        //Enable sanity metern UI
        
        //Disablar main meny dokumentet så att det inte syns längre.
        document.enabled = false;
        sanityDocument.enabled = true;






    }

    private void OnAllButtonsClick(ClickEvent clickEvent)
    {
        Debug.Log("You pressed the Start Button");
        
    }
}
