using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerCameraLook : MonoBehaviour
{
    Transform playerTransform;
    public Transform cameraTransform;
    float LookSensitivity;
    float cameraRotation; //Kameran ska komma ihåg sin vinkel

    float interactDistance;

    //Interact inspect grejer under
    public GameObject player;
    public Transform holdPos;

    public float throwForce = 100f;
    private float rotationSensitivity = 0.8f;
    private GameObject heldObject; //Objektet som man tar upp
    private Rigidbody heldObjectRgb; //och dess rigidbody
    private bool canDrop = true; //används för att undvika att släppa objekt när vi roterar
    private int LayerNumber; //Layer index där objekt vi håller i renderas

    private bool canLookAround = true;

    //Flashlight under
    [SerializeField] private Light flash;

    void Start()
    {
        playerTransform = GetComponent<Transform>();
        LookSensitivity = 300f;
        Cursor.lockState = CursorLockMode.Locked;
        cameraRotation = 0f;

        interactDistance = 3f;

        //Interact inspect grejer under
        LayerNumber = LayerMask.NameToLayer("HoldLayer");
    }

    void Update()
    {
        if(canLookAround)
        {
            MouseLookLeftnRight();
            MouseLookUpnDown();
        }
        SendInteractRay();
        if (heldObject != null) HoldingObject();

        FlashLight();
    }

    void MouseLookLeftnRight()
    {

        float mouse = Input.GetAxis("Mouse X") * Time.deltaTime * LookSensitivity; //Get axis får ett värde på hur musens X axeln har
        //rört sig mot förra frame'en

        playerTransform.Rotate(Vector3.up * mouse);
        flash.transform.rotation = playerTransform.rotation;
    }

    void MouseLookUpnDown()
    {
        float mouse = Input.GetAxis("Mouse Y") * Time.deltaTime * LookSensitivity;
        cameraRotation -= mouse; //måste flippa värdet
        cameraRotation = Mathf.Clamp(cameraRotation, -90, 90);
        cameraTransform.localRotation = Quaternion.Euler(cameraRotation, 0, 0); //Local transform eftersom vi vill rotera
        flash.transform.localRotation = cameraTransform.localRotation;
        //kameran med hänsyn till "parent" Player
    }

    void SendInteractRay()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            //Debug stråle
            Debug.DrawRay(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward) * interactDistance, Color.red, 2f);

            if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, interactDistance))
            {
                I_Interactable interactable = hit.collider.GetComponent<I_Interactable>() ?? hit.collider.GetComponentInParent<I_Interactable>();

                if(interactable != null && hit.transform.gameObject.tag == "canPickUp" && heldObject == null) //Om den är interactable, samt har pickup tag, så tar vi upp den istället för att trigga deras interact grej som troligen är tom
                    //heldObject == null är för att se till så att man icke håller något
                {
                    PickUpObject(hit.transform.gameObject);
                }
                else if(interactable != null && heldObject == null) //Om den inte har pickup taggen, men fortfarande kan interactas, så kallar man på interact funktionen
                {
                    interactable.Interact();
                }
                
            }
            else //för att droppa
            {
                if(canDrop == true)
                {
                    AvoidClipping();
                    DropObject();
                }
            }

        }
    }

    void HoldingObject()
    {
        MoveObject();
        RotateObject();
        if(Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true)
        {
            AvoidClipping();
            ThrowObject();
        }
    }

    void PickUpObject(GameObject pickUpObject)
    {
        if(pickUpObject.GetComponent<Rigidbody>()) //för att se till så att den har en rigidbody
        {
            heldObject = pickUpObject;
            heldObjectRgb = pickUpObject.GetComponent<Rigidbody>();
            heldObjectRgb.isKinematic = true;
            heldObjectRgb.transform.parent = holdPos.transform;
            heldObject.layer = LayerNumber;
            Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), true); // så att spelaren inte collide'ar med objektet
        }
    }
    void DropObject()
    {
        Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObject.layer = 0;
        heldObjectRgb.isKinematic = false;
        heldObject.transform.parent = null; //tar bort parent
        heldObject = null; // släpp gameobject
    }
    void MoveObject()
    {
        heldObject.transform.position = holdPos.transform.position;
    }
    void RotateObject()
    {
        if(Input.GetKey(KeyCode.R)) //om man håller R
        {
            canDrop = false; //så vi inte råkar släppa
            canLookAround = false;

            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;

            heldObject.transform.Rotate(Vector3.down, XaxisRotation);
            heldObject.transform.Rotate(Vector3.right, YaxisRotation);
        }
        else //så att man kan kolla runt igen
        {
            canDrop = true;
            canLookAround = true;
        }
    }
    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObject.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObject.layer = 0;
        heldObjectRgb.isKinematic = false;
        heldObject.transform.parent = null;
        heldObjectRgb.AddForce(transform.forward * throwForce);
        heldObject = null;
    }
    void AvoidClipping() //Används så att saker inte fastnar
    {
        var clipRange = Vector3.Distance(heldObject.transform.position, cameraTransform.position);

        RaycastHit[] hits; //Måste använda många pga att objektet vi håller blockar raycast framför oss
        hits = Physics.RaycastAll(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), clipRange);
        if (hits.Length > 1) //alltså måste träffa mer änm bara det objekt vi håller
        {
            heldObject.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //undvik droppa över spelaren
        }
    }

    void FlashLight()
    {
        flash.enabled = 50 < SanityMeter.Instance.sanityLevel; //50 ska vara hälften av sanitylevel
    }
}
