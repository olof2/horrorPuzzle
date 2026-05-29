using UnityEngine;
using UnityEngine.UIElements;

public class PickedUpHud : MonoBehaviour
{
    private UIDocument pickedUpHudDoc;
    private VisualElement visualElement;

    private KeyPickup keyPickup;
    public PlayerCameraLook playerCameraLook;


    private void Awake()
    {
        pickedUpHudDoc = GetComponent<UIDocument>();
        keyPickup = FindAnyObjectByType<KeyPickup>();
        playerCameraLook = FindAnyObjectByType<PlayerCameraLook>();

        pickedUpHudDoc.rootVisualElement.style.display = DisplayStyle.None;

        
    }

    private void OnEnable()
    {
        var root = pickedUpHudDoc.rootVisualElement;
        visualElement = root.Q<VisualElement>("Container");

    }
    // Update is called once per frame
    void Update()
    {
        if (playerCameraLook.pickedUp)
            ShowUI();
        else
            HideUI();

    }

  

    public void ShowUI()
    {
        if (pickedUpHudDoc == null)
        {
            Debug.LogError("PickedUpHudDoc is not assigned.");
            return;
        }
        if(pickedUpHudDoc != null)
        pickedUpHudDoc.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    private void HideUI()
    {
        if (pickedUpHudDoc == null)
        {
            Debug.LogError("PickedUpHudDoc is not assigned.");
            return;
        }
        if (pickedUpHudDoc != null)
            pickedUpHudDoc.rootVisualElement.style.display = DisplayStyle.None;
    }
}
