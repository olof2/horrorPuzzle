using UnityEngine;
using UnityEngine.UIElements;

public class InteractableHud : MonoBehaviour
{
    private UIDocument interactableHud;
    private Button ButtonE;
    private PlayerCameraLook playerCameraLook;
    private MainMenyEvents mainMenyEvents;

    private float interactDistance;
    public bool isPaused;


    //private void Start()
    //{
    //    playerCameraLook = FindAnyObjectByType<PlayerCameraLook>();
    //    interactableHud = GetComponent<UIDocument>();
    //    interactableHud.rootVisualElement.style.display = DisplayStyle.None;
    //    interactDistance = 3f;

    //}

    private void Awake()
    {
        interactableHud = GetComponent<UIDocument>();
        playerCameraLook = FindAnyObjectByType<PlayerCameraLook>();

        interactableHud.rootVisualElement.style.display = DisplayStyle.None;
        interactDistance = 3f;
    }

    private void OnEnable()
    {
        if (interactableHud == null)
            interactableHud = GetComponent<UIDocument>();
        var root = interactableHud.rootVisualElement;
        ButtonE = root.Q("E") as Button;



        //Regristerar callbacks för knapparna i pausmenyn, UnPaused() och OnExitGameClick() metoderna kommer att köras när knapparna klickas på.
    }

    void Update()
    {
       
            ToggleInteractableHud();

        

      

    }

    //Denna metod kollar om spelaren tittar på ett interagerbart objekt inom en viss räckvidd, och visar eller döljer interaktions-HUD:en baserat på det.
    private void ToggleInteractableHud()
    {
        if (isPaused)
        {
                if (interactableHud != null)
                    interactableHud.rootVisualElement.style.display = DisplayStyle.None;
                return;
        }



        RaycastHit hit;

            

            if (Physics.Raycast(playerCameraLook.cameraTransform.position, playerCameraLook.cameraTransform.TransformDirection(Vector3.forward), out hit, interactDistance))
            {
                I_Interactable interactable = hit.collider.GetComponent<I_Interactable>() ?? hit.collider.GetComponentInParent<I_Interactable>();
                if (interactable != null && interactDistance <= 3)
                {
                    interactableHud = GetComponent<UIDocument>();
                    interactableHud.enabled = true;
                    interactableHud.rootVisualElement.style.display = DisplayStyle.Flex;
                    interactableHud.transform.position = playerCameraLook.cameraTransform.position + playerCameraLook.cameraTransform.forward * 2f; //Positionerar hud:en framför kameran
                }
                 if (Input.GetKeyDown(KeyCode.E))
                {
                interactableHud.rootVisualElement.style.display = DisplayStyle.None;
                Debug.Log("E key pressed från InteractableScript");
                 }
            
            }
        else
        {
            if (interactableHud != null)
                interactableHud.rootVisualElement.style.display = DisplayStyle.None;
        }


    }

    public void HideUI()
    {
        if (interactableHud != null)
            interactableHud.rootVisualElement.style.display = DisplayStyle.None;
        interactableHud.enabled = false;
    }
    public void ShowUI()
    {
        if (interactableHud != null)
            interactableHud.rootVisualElement.style.display = DisplayStyle.Flex;
        interactableHud.enabled = true;
    }
}