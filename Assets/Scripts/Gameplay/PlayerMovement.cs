using Unity.VisualScripting;
using UnityEngine;


//public class PlayerMovement : MonoBehaviour
public class PlayerMovement : Singleton<PlayerMovement>
{
    Transform playerTransform;
    CharacterController chara;
    public float speed;
    public float runSpeed;
    public bool canMove = true;
    //public bool canRotate = true;

    public float gravity = -9.81f;
    float velocityY;

    //bool för om overtime is active (= true när sanity 100%)
    private bool isOvertime = false;


    [HideInInspector] public Vector3 currentInput;   
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        chara = GetComponent<CharacterController>();
        speed = 2.3f;
        runSpeed = 5f;

        //failsafe
        if (SanityMeter.Instance == null) return;
        SanityMeter.Instance.OnReached100 += UnlockOvertime;
    }

    //metod för att activera overtime bool
    private void UnlockOvertime()
    {
        isOvertime = true;
    }



    void Update()
    {
        if (chara.isGrounded && velocityY < 0)
        {
            velocityY = -2f;
        }

        //if sats för player movement under overtime (overtime = 100% sanity, speed increase)
        if (canMove && isOvertime)
        {
            currentInput = GetDirectionVector();

            Vector3 movement = currentInput.z * playerTransform.forward + currentInput.x * playerTransform.right; //WIP
            //playerTransform.position += movement * speed * Time.deltaTime;

            velocityY += gravity * Time.deltaTime;
            movement.y = velocityY;

            chara.Move(movement * runSpeed * Time.deltaTime);
        }
        else if (canMove)
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

    //Unsubscribe till events vid avslutning av spelet.
    private void OnDestroy()
    {
        //failsafe
        if (SanityMeter.Instance == null) return;
        SanityMeter.Instance.OnReached100 -= UnlockOvertime;
    }
}
