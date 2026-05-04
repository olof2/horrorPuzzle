using UnityEngine;
using UnityEngine.UIElements;

public class InteractableHud : Singleton<InteractableHud>
{
    public UIDocument interactableHud;
    private Button ButtonE;
    public VisualElement visualElement;
    public PlayerCameraLook playerCameraLook;
    private Camera cam;
    private Door door;

    private Transform target;
    //public Transform UIAnchor;

    [SerializeField]
    private float interactDistance = 3f;
    public bool isPaused;


   

    protected override void Awake()
    {
        base.Awake();
        //TryInitUI();
        if (Instance != this)
        {
            return;
        }
        if (interactableHud == null ) 
        interactableHud = GetComponent<UIDocument>();
        if (playerCameraLook == null )
            playerCameraLook = FindAnyObjectByType<PlayerCameraLook>();
        
        // Hämtar playerCameraLook camera
        cam = playerCameraLook.GetComponentInChildren<Camera>();
        //HideUI();
        //playerCameraLook = FindAnyObjectByType<PlayerCameraLook>();

        //    if (interactableHud != null)
        //        interactableHud.rootVisualElement.style.display = DisplayStyle.None;
        //    interactDistance = 3f;
    }

    private void OnEnable()
    {
        //TryInitUI();
       
        //var root = interactableHud.rootVisualElement;
        //ButtonE = root.Q<Button>("E");
        TryInitUI();

        //Regristerar callbacks för knapparna i pausmenyn, UnPaused() och OnExitGameClick() metoderna kommer att köras när knapparna klickas på.
    }

    private void TryInitUI()
    {

        if (interactableHud == null)
            interactableHud = GetComponent<UIDocument>();
        var root = interactableHud.rootVisualElement;
        visualElement = root.Q<VisualElement>("Container");

        ButtonE = visualElement.Q<Button>("E");

       if (visualElement != null)
            visualElement.style.display = DisplayStyle.None;


    }

    void Update()
    {
      

        ToggleInteractableHud();

  
    }
    

    //Denna metod kollar om spelaren tittar på ett interagerbart objekt inom en viss räckvidd, och visar eller döljer interaktions-HUD:en baserat på det.
    public void ToggleInteractableHud()
    {
        if (isPaused)
        {
            HideUI();
            return;
        }

        if (playerCameraLook == null || playerCameraLook.cameraTransform == null)
        {
            Debug.LogWarning("PlayerCameraLook or its cameraTransform is not assigned.");
            return;
        }

        RaycastHit hit;

        if (Physics.Raycast(playerCameraLook.cameraTransform.position, playerCameraLook.cameraTransform.TransformDirection(Vector3.forward), out hit, interactDistance))
        {
            I_Interactable interactable = hit.collider.GetComponent<I_Interactable>() ?? hit.collider.GetComponentInParent<I_Interactable>();
            if (interactable != null)
            {

               // target = interactable.UIAnchor;

                ShowUI();


                if (Input.GetKeyDown(KeyCode.E))
                {
                    HideUI();
                    // interactableHud.rootVisualElement.style.display = DisplayStyle.None;
                    Debug.Log("E key pressed från InteractableScript");
                }

                return;
            }
            else
                HideUI();
            


        }
        HideUI();
    }

    public void HideUI()
    {
      
        if (visualElement != null)
        {
            visualElement.style.display = DisplayStyle.None;
            return;
        }

       
    }
    public void ShowUI()
    {

        var panel = interactableHud.rootVisualElement.panel;

        if (visualElement != null)
        {
            visualElement.style.display = DisplayStyle.Flex;
            //visualElement.style.position = Position.Absolute;
            //visualElement.style.display = DisplayStyle.Flex;
            //Vector2 pos = RuntimePanelUtils.CameraTransformWorldToPanel(panel, target.position, cam); // Gör UI pos till worldspace pos med hjälp av target pos och playerLookCamera cam
            //float panelHeigth = panel.visualTree.layout.height;
            //visualElement.style.left = pos.x;
            //visualElement.style.top = panelHeigth - pos.y;

            //Debug.Log("World space pos = " + pos);
            return;
        }


    }
}