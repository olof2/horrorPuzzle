using UnityEngine;

public class PlayerCameraLook : MonoBehaviour
{
    Transform playerTransform;
    public Transform cameraTransform;
    float LookSensitivity;
    float cameraRotation; //Kameran ska komma ihåg sin vinkel

    float interactDistance;
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        LookSensitivity = 300f;
        Cursor.lockState = CursorLockMode.Locked;
        cameraRotation = 0f;

        interactDistance = 3f;
    }

    void Update()
    {
        MouseLookLeftnRight();
        MouseLookUpnDown();
        SendInteractRay();
    }

    void MouseLookLeftnRight()
    {

        float mouse = Input.GetAxis("Mouse X") * Time.deltaTime * LookSensitivity; //Get axis får ett värde på hur musens X axeln har
        //rört sig mot förra frame'en

        playerTransform.Rotate(Vector3.up * mouse);
    }

    void MouseLookUpnDown()
    {
        float mouse = Input.GetAxis("Mouse Y") * Time.deltaTime * LookSensitivity;
        cameraRotation -= mouse; //måste flippa värdet
        cameraRotation = Mathf.Clamp(cameraRotation, -90, 90);
        cameraTransform.localRotation = Quaternion.Euler(cameraRotation, 0, 0); //Local transform eftersom vi vill rotera
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

                if(interactable != null)
                {
                    interactable.Interact();
                }
            }

        }
    }
}
