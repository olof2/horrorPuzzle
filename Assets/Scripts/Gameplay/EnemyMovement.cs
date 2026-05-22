using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform target; // The target the enemy will move towards
    [SerializeField] private float speed = 2f; // The speed at which the enemy moves
    [SerializeField] private bool isMoving = false;
    private Vector3 direction;

    private GameOverScript gameOverScript;
    public PlayerMovement playerMovement;
    public PlayerCameraLook playerCamera;
    public CameraHelper cameraHelper;

    //ljud effekter
    private AudioSource audioSource;
    public AudioClip jumpScare;

    //jumpscare sequence timer
    private bool deathSequenceStarted = false;
    private float deathTimer = 5f;

    private bool gameOver = false;
    public bool Game_Over {  get { return gameOver; } }


    void Start()
    {
        if (SanityMeter.Instance != null)
        {
            //MoveForward metoder för när spöket ska flyttas fram varje "sanity nivå"
            SanityMeter.Instance.OnReached25 += MoveForward25;
            SanityMeter.Instance.OnReached50 += MoveForward50;
            SanityMeter.Instance.OnReached75 += MoveForward75;
            SanityMeter.Instance.OnReached95 += MoveForward95;

            // Start moving when sanity reaches 100
            SanityMeter.Instance.OnReached100 += StartMoving;
        }
        else
        {
            Debug.LogError("SanityMeter instance not found!");
        }


    }

    void Update()
    {
        transform.LookAt(target); // Rotate the enemy to face the target
        direction = (target.position - transform.position).normalized; // Calculate the direction towards the target


        if (isMoving && PlayerMovement.Instance.enabled)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        //för death animation/ sequence
        if (deathSequenceStarted)
        {
            //player camera kollar på spöket + 0.5f "down"
            Vector3 lookPosition = transform.position + Vector3.down * 0.5f;

            //Quaternion används för att göra "look" mer "smooth" (istället för instant "snap"
            //till spöket så är det "smooth" rotation. 10f är hastigheten för rotation)
            Quaternion targetRotation = Quaternion.LookRotation(lookPosition - playerCamera.transform.position);
            playerCamera.transform.rotation = Quaternion.Slerp(playerCamera.transform.rotation, targetRotation, 15f * Time.deltaTime);

            //räknar ner timern
            deathTimer -= Time.deltaTime;

            //om deathTimer är = 0, GameOver
            if (deathTimer <= 0)
            {
                gameOver = true;
            }
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (deathSequenceStarted) return;   //failsafe

        if (other.CompareTag("Player"))
        {
            //när kollision händer, deathSequence bool blir true 
            deathSequenceStarted = true;

            //flytar upp spöket lite (spöket är mindre än playern)
            transform.position += Vector3.up * 1f;

            //Stoppar player och ghost movement vid collision
            isMoving = false;
            playerMovement.canMove = false;

            //stoppar player camera control vid collision
            playerCamera.enabled = false;

            //höjer camera shake vid jumpscare
            cameraHelper.SetJumpscareShake(true);

            //Jumpscare audio spelas vid collision
            audioSource.PlayOneShot(jumpScare);

            Debug.Log("Enemy collided with the player and stopped moving.");
        }
    }

    private void StartMoving()
    {
        isMoving = true; // Start moving when sanity reaches 25
        Debug.Log("Enemy started moving towards the player.");
    }

    //flyttar fram spöket 4f för varje "MoveForward" metod
    //"back" eftersom det går egentligen ner i Z position
    private void MoveForward25()
    {
        transform.position += Vector3.back * 2f;
    }
    private void MoveForward50()
    {
        transform.position += Vector3.back * 2f;
    }
    private void MoveForward75()
    {
        transform.position += Vector3.back * 4f;
    }
    private void MoveForward95()
    {
        transform.position += Vector3.back * 4f;
    }

    void OnDestroy()
    {
        if (SanityMeter.Instance != null)
        {
            //Unsubscribe
            SanityMeter.Instance.OnReached25 -= MoveForward25;
            SanityMeter.Instance.OnReached50 -= MoveForward50;
            SanityMeter.Instance.OnReached75 -= MoveForward75;
            SanityMeter.Instance.OnReached95 -= MoveForward95;

            // Unsubscribe from the event when the enemy is destroyed
            SanityMeter.Instance.OnReached100 -= StartMoving;
        }
    }
}
