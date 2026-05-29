using PadlockSystem;
using UnityEngine;
using UnityEngine.UIElements;

public class VictoryMenuScript : MonoBehaviour
{

    private UIDocument victoryMenuDoc;
    private VisualElement visualElement;
    private PlayerCameraLook playerCameraLook;

    private Door door;
    PadlockItem padlockItem;


    private void Awake()
    {
        victoryMenuDoc = GetComponent<UIDocument>(); // Hämtar UI Dokumentet
        door = FindAnyObjectByType<Door>(); // Hittar dörren i scenen
        visualElement.style.display = DisplayStyle.None; // gömmer UI i awake
    }

    private void OnEnable()
    {
        var root = victoryMenuDoc.rootVisualElement;
        visualElement = root.Q<VisualElement>("Container");
    }

    // Update is called once per frame
    void Update()
    {
        //Nä slår in rätt kod och då kommer öppna "rätt" dörr, visa UI
      
        



    }


    public void ShowUI()
    {
        
        if (visualElement != null)
            visualElement.style.display = DisplayStyle.Flex; // Sätter UI synlig
    }

    public void HideUI()
    {
        if (visualElement != null)
            visualElement.style.display = DisplayStyle.None; // Gömmer UI
    }
}
