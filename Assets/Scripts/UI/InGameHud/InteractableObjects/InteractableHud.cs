using UnityEngine;
using UnityEngine.UIElements;

public class InteractableHud : Singleton<InteractableHud>
{
    public UIDocument interactableHud;
    private Button ButtonE;
    private Button Button_E_PickUp;

    public VisualElement rootVisual;
    private VisualElement openElement;
    private VisualElement closeElement;
    private VisualElement pickUpElement;
    private VisualElement rotateElement;
    private VisualElement lockedElement;

    public VictoryMenuScript victoryMenuScript;

    public PlayerCameraLook playerCameraLook;

    private Camera cam;
   // private Door door;


    public PuzzlePickup puzzlePickup;
   
    private GameOverScript gameOverScript;
    KeyPickup keyPickup;
    public IPanel panel;
    I_Interactable currentInteractable = null;

    [SerializeField]
    private Transform target;
    //public Transform UIAnchor;

    [SerializeField]
    private float interactDistance = 3f;
    public bool isPaused;

    bool layoutReady = false; // Bool för att kolla om layouten är klar, så att vi inte försöker placera UI innan den har fått sina dimensioner osv, vilket gör att den hamnar fel, i 0,0 osv;
    bool uiVisiable = false; //Bool för att kolla om UI är synligt




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

        victoryMenuScript = GetComponent<VictoryMenuScript>();

       // door = FindAnyObjectByType<Door>();

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
        rootVisual = interactableHud.rootVisualElement;
        openElement = rootVisual.Q<VisualElement>("Open");
        pickUpElement = rootVisual.Q<VisualElement>("PickUp");
        rotateElement = rootVisual.Q<VisualElement>("Rotate");
        closeElement = rootVisual.Q<VisualElement>("Close");
        lockedElement = rootVisual.Q<VisualElement>("Locked");

        ButtonE = openElement.Q<Button>("E");
        Button_E_PickUp = pickUpElement.Q<Button>("E_PickUp");
        if (openElement != null)
            openElement.style.display = DisplayStyle.None;
        if (pickUpElement != null)
            pickUpElement.style.display = DisplayStyle.None;
        if (rotateElement != null)
            rotateElement.style.display = DisplayStyle.None;
        if (closeElement != null)
            closeElement.style.display = DisplayStyle.None;
        if (lockedElement != null)
            lockedElement.style.display = DisplayStyle.None;

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
        if (!layoutReady || !uiVisiable || openElement == null || closeElement == null || lockedElement == null || rotateElement == null || pickUpElement == null || target == null)
            return;

        //Kallar på metoden och placerar ut UI;
        UpdateUIPos();
    }


    //Denna metod kollar om spelaren tittar på ett interagerbart objekt inom en viss räckvidd, och visar eller döljer interaktions-HUD:en baserat på det.
    public void ToggleInteractableHud()
    {
        if (isPaused)
        {
            HideAllUI();
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

                // OM det inte är samma interagerbara objekt som förra frame, så uppdatera currentInteractable, target och visa UI:n
                if (currentInteractable != interactable)
                {
                    // Sätter currentInteractable till det nya objektet som spelaren tittar på.
                    //target = interactable.UIAnchor;
                    currentInteractable = interactable;
                    target = interactable.UIAnchor; // Sätter target till det nya objektets UIAnchor, så att UI:n kommer att placeras vid det objektet
                    // Kollar om det är en door, puzzle, pick up etc och kallar då på rätt UI metod
                    if (currentInteractable is PuzzlePickup)
                    {

                        
                        ShowPickUpUI();

                        //if (playerCameraLook.pickedUp)
                        //    pickUpElement.style.display = DisplayStyle.None; // Döljer PickUp UI

                        //keyPickup = interactable as KeyPickup;
                    }
                    else if (currentInteractable is Door) // Om interactable är en dörr
                    {
                        //uiVisiable = true;
                        Door door = currentInteractable as Door; // Hämta specifika dörren som spelaren tittar på 

                        if (door.isLocked) // Kollar om dörren är låst
                        {
                            ShowLockedUI();

                        }

                        else if (door.isOpen) // Kollar om dörren är öppen
                        {
                            //door.isLocked = false;
                            ShowCloseUI();
                        }
                        


                        else
                            ShowOpenUI();
                        // Om den inte är öppen eller låst, så måste den vara st




                        //door = interactable as Door;
                    }
                    else  if (currentInteractable is PuzzlePlate)
                    {
                        // target = interactable.UIAnchor; // Sätter target till det nya objektets UIAnchor, så att UI:n kommer att placeras vid det objektet
                        //uiVisiable = true;
                        ShowRotateUI();
                        // return;
                    }

                    //uiVisiable = true;
                    //ShowUI();

                }


                if (Input.GetKeyDown(KeyCode.E))
                {
                    //HideUI();
                    // interactableHud.rootVisualElement.style.display = DisplayStyle.None;
                    Debug.Log("E key pressed från InteractableScript");
                }

                return;
            }
            //else
              //  HideUI();
            


        }
        // OM Raycasten inte träffa någt objekt man kan interacta med, sett den till null och dölj UI:n
        if (currentInteractable != null)
        {
            currentInteractable = null;
            HideAllUI();


        }
        //HideUI();
    }

    // Metoden döljer all UI
    public void HideAllUI()
    {
      
        if (rootVisual != null)
        {
            rootVisual.style.display = DisplayStyle.None;
            layoutReady = false; // layout blir false
            uiVisiable = false; // Så att UI inte uppdateras i lateUpdate
            if (openElement != null)
                openElement.style.display = DisplayStyle.None;
            if (pickUpElement != null)
                pickUpElement.style.display = DisplayStyle.None;
            if (rootVisual != null)
                rootVisual.style.display = DisplayStyle.None;
            if (rotateElement != null)
                rotateElement.style.display = DisplayStyle.None;
            if (closeElement != null)
                closeElement.style.display = DisplayStyle.None;
            if (lockedElement != null)
                lockedElement.style.display = DisplayStyle.None;


            return;
        }

       
    }

     //Metoden gör så att Open UI elelemnt visas
    public void ShowOpenUI()
    {
        if (panel == null)
            Debug.LogError("PANEL är null");

        if (cam == null)
            Debug.LogError("Cam är null");

        if (target == null)
            Debug.LogError("Target är null");


        if (rootVisual != null)
        {
            // UI är synligt men layout är inte klar än
            uiVisiable = true;
            layoutReady = false;
            //var root = interactableHud.rootVisualElement;
             //visualElement = root.Q<VisualElement>("E");
            //Visar UI:n
            rootVisual.style.display = DisplayStyle.Flex;

            if (pickUpElement != null)
                pickUpElement.style.display = DisplayStyle.None; // Döljer PickUp UI
            if (rotateElement != null)
                rotateElement.style.display = DisplayStyle.None; // Döljer Rotate UI
            if (closeElement != null)
                closeElement.style.display = DisplayStyle.None; // Döljer Close UI
            if (lockedElement != null)
                lockedElement.style.display = DisplayStyle.None; // Döljer Locked UI
            if (openElement != null)
                openElement.style.display = DisplayStyle.Flex; //Visar Open UI

            // Sätter absolut pos så att left/top avgör pos, inte vart det ligger i panelen i UI Toolkit
            rootVisual.style.position = Position.Absolute;

            //Körs nästa frame, så att layouten hinner regristreras så att UI:n inte får 0,0 dimensioner
            rootVisual.schedule.Execute(() =>
            {
                layoutReady = true;
            });

            Debug.Log($"OpenUI size: {rootVisual.layout.width} x {rootVisual.layout.height}");
           // Debug.Log("World space pos = " + pos);
            return;
        }


    }

    //Metode gör så att Pick Up UI visas
    public void ShowPickUpUI()
    {
        if (panel == null)
            Debug.LogError("PANEL är null");
        if (cam == null)
            Debug.LogError("Cam är null");
        if (target == null)
            Debug.LogError("Target är null");

        if (rootVisual != null)
        {
            uiVisiable = true; // UI är synligt
            layoutReady = false; 

           // var root = interactableHud.rootVisualElement;
               // rootVisual = root.Q<VisualElement>("E_PickUp");
    
             rootVisual.style.display = DisplayStyle.Flex; // Visar VisualElementet som innehpller UI

            if (openElement != null)
                openElement.style.display= DisplayStyle.None; //Döljer Open UI
            if (rotateElement != null)
                rotateElement.style.display = DisplayStyle.None; // Döljer Rotate UI
            if (closeElement != null)
                closeElement.style.display = DisplayStyle.None; // Döljer Close UI
            if (lockedElement != null)
                lockedElement.style.display = DisplayStyle.None; // Döljer Locked UI
            if (pickUpElement != null)
                pickUpElement.style.display = DisplayStyle.Flex; //Visar PickUp UI

            // visualElement.style.display = DisplayStyle.Flex;

            rootVisual.style.position = Position.Absolute; //Sätter absolut pos så att left/top avgör pos, inte vart det ligger i panelen i UI Toolkit

            rootVisual.schedule.Execute(() =>
            {
                layoutReady = true;
            });

                Debug.Log($"PuckUpUI size: {rootVisual.layout.width} x {rootVisual.layout.height}");
                //Debug.Log("World space pos = " + pos);
                return;

        }
    }

    public void ShowRotateUI()
    {
        if (panel == null)
            Debug.LogError("PANEL är null");
        if (cam == null)
            Debug.LogError("Cam är null");
        if (target == null)
            Debug.LogError("Target är null");
        if (rootVisual != null)
        {
            uiVisiable = true; // UI är synligt
            layoutReady = false;
            rootVisual.style.display = DisplayStyle.Flex; // Visar VisualElementet som innehpller UI
            if (openElement != null)
                openElement.style.display = DisplayStyle.None; //Döljer Open UI
            if (pickUpElement != null)
                pickUpElement.style.display = DisplayStyle.None; // Döljer PickUp UI
            if (closeElement != null)
                closeElement.style.display = DisplayStyle.None; // Döljer Close UI
            if (lockedElement != null)
                lockedElement.style.display = DisplayStyle.None; // Döljer Locked UI
            if (rotateElement != null)
                rotateElement.style.display = DisplayStyle.Flex; //Visar Rotate UI
            rootVisual.style.position = Position.Absolute; //Sätter absolut pos så att left/top avgör pos, inte vart det ligger i panelen i UI Toolkit
            rootVisual.schedule.Execute(() =>
            {
                layoutReady = true;
            });
            Debug.Log($"RotateUI size: {rootVisual.layout.width} x {rootVisual.layout.height}");
            //Debug.Log("World space pos = " + pos);
            return;
        }
    }

    public void ShowCloseUI()
    {
        if (panel == null)
            Debug.LogError("PANEL är null");
        if (cam == null)
            Debug.LogError("Cam är null");
        if (target == null)
            Debug.LogError("Target är null");
        if (rootVisual != null)
        {
            uiVisiable = true; // UI är synligt
            layoutReady = false;
            rootVisual.style.display = DisplayStyle.Flex; // Visar VisualElementet som innehpller UI
            if (openElement != null)
                openElement.style.display = DisplayStyle.None; //Döljer Open UI
            if (pickUpElement != null)
                pickUpElement.style.display = DisplayStyle.None; // Döljer PickUp UI
            if (rotateElement != null)
                rotateElement.style.display = DisplayStyle.None; //Visar Rotate UI
            if (lockedElement != null)
                lockedElement.style.display = DisplayStyle.None; // Döljer Locked UI
            if (closeElement != null)
                closeElement.style.display = DisplayStyle.Flex; //Visar Rotate UI

            rootVisual.style.position = Position.Absolute; //Sätter absolut pos så att left/top avgör pos, inte vart det ligger i panelen i UI Toolkit
            rootVisual.schedule.Execute(() =>
            {
                layoutReady = true;
            });
            Debug.Log($"CloseUI size: {rootVisual.layout.width} x {rootVisual.layout.height}");
            //Debug.Log("World space pos = " + pos);
            return;
        }
    }

    public void ShowLockedUI()
    {
        if (panel == null)
            Debug.LogError("PANEL är null");
        if (cam == null)
            Debug.LogError("Cam är null");
        if (target == null)
            Debug.LogError("Target är null");
        if (rootVisual != null)
        {
            uiVisiable = true; // UI är synligt
            layoutReady = false;
            rootVisual.style.display = DisplayStyle.Flex; // Visar VisualElementet som innehpller UI
            if (openElement != null)
                openElement.style.display = DisplayStyle.None; //Döljer Open UI
            if (pickUpElement != null)
                pickUpElement.style.display = DisplayStyle.None; // Döljer PickUp UI
            if (rotateElement != null)
                rotateElement.style.display = DisplayStyle.None; //Visar Rotate UI
            if (closeElement != null)
                closeElement.style.display = DisplayStyle.None; // Döljer Close UI
            if (lockedElement != null)
                lockedElement.style.display = DisplayStyle.Flex; //Visar Locked UI

            rootVisual.style.position = Position.Absolute; //Sätter absolut pos så att left/top avgör pos, inte vart det ligger i panelen i UI Toolkit
            rootVisual.schedule.Execute(() =>
            {
                layoutReady = true;
            });
            Debug.Log($"LockedUI size: {rootVisual.layout.width} x {rootVisual.layout.height}");
            //Debug.Log("World space pos = " + pos);
            return;
        }
    }

    // Uppdaterar UI:s position varje frame i LateUpdate, så att den följer med objektet i worldspace
    // Den gör om world pos till panel-kordinater, UI Toolkits kordinatsystem, och placerar ut UI:n där
    public void UpdateUIPos()
    {
        // Hämmtar world pos för target. Target har ett empty gameObject UIAnchor på sig
        Vector3 worldPos = target.position;

       

        // Gör om world pos till panel-kordinater, UI Toolkits kordinatsystem.
        Vector2 pos = RuntimePanelUtils.CameraTransformWorldToPanel(panel, worldPos, cam); 
        //Hämtar UI:s width och heigth
        float openElementWidht = openElement.layout.width;
        float openElementHeight = openElement.layout.height;
        //Hämtar PuckUp UI width och height
        float w2 = pickUpElement.layout.width;
        float h2 = pickUpElement.layout.height;

        float w3 = rotateElement.layout.width;
        float h3 = rotateElement.layout.height;

        float w4 = closeElement.layout.width;
        float h4 = closeElement.layout.height;

        float lockedElementWidth = lockedElement.layout.width;
        float lockedElementHeigth = lockedElement.layout.height;
        //Tvingar UI Toolkit att placera ut UIs mittpunkt i worldspace.
        // UI Toolkit placerar normalt utifrån vänster hörn, så vi måste dra av halva width och height för att få mittpunkten på rätt ställe.
        openElement.style.left = pos.x - openElementWidht * 0.5f;//Open UI
        openElement.style.top = pos.y - openElementHeight * 0.5f;//Open UI

        pickUpElement.style.left = pos.x - w2 * 0.5f;//PickUp UI
        pickUpElement.style.top = pos.y - h2 * 0.5f;//PickUp UI

        rotateElement.style.left = pos.x - w3 * 0.5f;//Rotate UI
        rotateElement.style.top = pos.y - h3 * 0.5f;//Roteate UI

        closeElement.style.left = pos.x - w4 * 0.5f;//Rotate UI
        closeElement.style.top = pos.y - h4 * 0.5f;//Roteate UI

        lockedElement.style.left = pos.x - lockedElementWidth * 0.5f;//Locked UI
        lockedElement.style.top = pos.y - lockedElementHeigth * 0.5f;//Locked UI

        Debug.Log($"UI size: {openElement.layout.width} x {openElement.layout.height}");
        Debug.Log($"PickUp UI size: {pickUpElement.layout.width} x {pickUpElement.layout.height}");

    }
}