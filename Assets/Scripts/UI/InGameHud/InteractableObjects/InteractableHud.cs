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
    private GameOverScript gameOverScript;
    public IPanel panel;
    I_Interactable currentInteractable = null;

    [SerializeField]
    private Transform target;
    //public Transform UIAnchor;

    [SerializeField]
    private float interactDistance = 3f;
    public bool isPaused;

    bool layoutReady = false;
    bool uiVisiable = false;




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
        visualElement = root.Q<VisualElement>("E");

        ButtonE = visualElement.Q<Button>("E");

       if (visualElement != null)
            visualElement.style.display = DisplayStyle.None;

        panel = interactableHud.rootVisualElement.panel;



    }

    //
    void Update()
    {
      

        ToggleInteractableHud();

  
    }

    //Körs efter alla UPDATE varje frame
    //Körs efter att kameran har flyttat sig
    //efter alla objects har uppdaterats
    // efter all fysik osv
    // Bra för UI för att kameran måste vara färdig flyttat innan man placerar WorldSpace UI, annars blir pos fel, UI "glider" osv;
    // 
    void LateUpdate()
    {
        //Om layour, ui inte visiable osv returnera o gör inget
        if (!layoutReady || !uiVisiable || visualElement == null || target == null)
            return;

        //Kallar på metoden och placerar ut UI;
        UpdateUIPos();
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


                if (currentInteractable != interactable)
                {   
                    currentInteractable = interactable;
                    target = interactable.UIAnchor;

                    uiVisiable = true;
                    ShowUI();

                }


                if (Input.GetKeyDown(KeyCode.E))
                {
                    HideUI();
                    // interactableHud.rootVisualElement.style.display = DisplayStyle.None;
                    Debug.Log("E key pressed från InteractableScript");
                }

                return;
            }
            //else
              //  HideUI();
            


        }

        if (currentInteractable != null)
        {
            currentInteractable = null;
            HideUI();

        }
        //HideUI();
    }

    public void HideUI()
    {
      
        if (visualElement != null)
        {
            visualElement.style.display = DisplayStyle.None;
            layoutReady = false; // layout blir false
            uiVisiable = false; // Så att UI inte uppdateras i lateUpdate
            return;
        }

       
    }

   
    public void ShowUI()
    {
        if (panel == null)
            Debug.LogError("PANEL är null");

        if (cam == null)
            Debug.LogError("Cam är null");

        if (target == null)
            Debug.LogError("Target är null");


        if (visualElement != null)
        {
            // UI är synligt men layout är inte klar än
            uiVisiable = true;
            layoutReady = false;

            //Visar UI:n
            visualElement.style.display = DisplayStyle.Flex;

            // Sätter absolut pos så att left/top avgör pos, inte vart det ligger i panelen i UI Toolkit
            visualElement.style.position = Position.Absolute;

            //Körs nästa frame, så att layouten hinner regristreras så att UI:n inte får 0,0 dimensioner
            visualElement.schedule.Execute(() =>
            {
                layoutReady = true;
            });

            Debug.Log($"UI size: {visualElement.layout.width} x {visualElement.layout.height}");
           // Debug.Log("World space pos = " + pos);
            return;
        }


    }

    public void UpdateUIPos()
    {
        // Hämmtar world pos för target. Target har ett empty gameObject UIAnchor på sig
        Vector3 worldPos = target.position;
     
        // Gör om world pos till panel-kordinater, UI Toolkits kordinatsystem.
        Vector2 pos = RuntimePanelUtils.CameraTransformWorldToPanel(panel, worldPos, cam); 
        //Hämtar UI:s width och heigth
        float w = visualElement.layout.width;
        float h = visualElement.layout.height;
        //Tvingar UI Toolkit att placera ut UIs mittpunkt i worldspace.
        visualElement.style.left = pos.x - w * 0.5f;
        visualElement.style.top = pos.y - h * 0.5f;
        Debug.Log($"UI size: {visualElement.layout.width} x {visualElement.layout.height}");

    }
}