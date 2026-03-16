using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform playerTransform;
    float speed;
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        speed = 5f;
    }

    
    void Update()
    {
        Vector3 movement = GetDirectionVector().z * playerTransform.forward + GetDirectionVector().x * playerTransform.right; //WIP
        playerTransform.position += movement * speed * Time.deltaTime;
    }

    Vector3 GetDirectionVector()
    {
        Vector3 vector = Vector3.zero;
        float FrontBack = Input.GetAxis("Vertical");
        float LeftRight = Input.GetAxis("Horizontal");
        if(FrontBack != 0) vector.z = FrontBack;
        if(LeftRight != 0) vector.x = LeftRight;
        return vector.normalized;
    }
}
