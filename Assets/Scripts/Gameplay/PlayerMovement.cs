using Unity.VisualScripting;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    Transform playerTransform;
    CharacterController chara;
    public float speed;
    public bool canMove = true;
    //public bool canRotate = true;

    public float gravity = -9.81f;
    float velocityY;

    [HideInInspector] public Vector3 currentInput;   
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        chara = GetComponent<CharacterController>();
        speed = 5f;
    }

    
    void Update()
    {
        if (chara.isGrounded && velocityY < 0)
        {
            velocityY = -2f;
        }

        if (canMove)
        {
            currentInput = GetDirectionVector();

            Vector3 movement = currentInput.z * playerTransform.forward + currentInput.x * playerTransform.right; //WIP
            //playerTransform.position += movement * speed * Time.deltaTime;

            velocityY += gravity * Time.deltaTime;
            movement.y = velocityY;

            chara.Move(movement * speed * Time.deltaTime);
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
