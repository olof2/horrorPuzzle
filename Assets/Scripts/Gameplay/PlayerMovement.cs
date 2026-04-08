using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    Transform playerTransform;
    public float speed;
    public bool canMove = true;
    //public bool canRotate = true;

    [HideInInspector] public Vector3 currentInput;   
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        speed = 5f;
    }

    
    void Update()
    {
        if (canMove)
        {
            currentInput = GetDirectionVector();

            Vector3 movement = GetDirectionVector().z * playerTransform.forward + GetDirectionVector().x * playerTransform.right; //WIP
            playerTransform.position += movement * speed * Time.deltaTime;
        }
    }

    Vector3 GetDirectionVector()
    {
        Vector3 vector = Vector3.zero;
        float FrontBack = Input.GetAxisRaw("Vertical");
        float LeftRight = Input.GetAxisRaw("Horizontal");
        if(FrontBack != 0) vector.z = FrontBack;
        if(LeftRight != 0) vector.x = LeftRight;
        return vector.normalized;
    }

    // Disables player control when needed (e.g., in cutscenes)
    public void SetPlayerDisableMode(bool active)
    {
        canMove = !active;
        //canRotate = !active;
    }
}
