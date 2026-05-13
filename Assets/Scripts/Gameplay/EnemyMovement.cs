using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform target; // The target the enemy will move towards
    [SerializeField] private float speed = 2f; // The speed at which the enemy moves
    [SerializeField] private bool isMoving = false;
    private Vector3 direction;

    private GameOverScript gameOverScript;

    private bool gameOver = false;
    public bool Game_Over {  get { return gameOver; } }


    void Start()
    {
        if (SanityMeter.Instance != null)
        {
            SanityMeter.Instance.OnReached95 += StartMoving;// Start moving when sanity reaches 95
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = false; // Stop moving when colliding with the player
            gameOver = true;

            Debug.Log("Enemy collided with the player and stopped moving.");
        }
    }

    private void StartMoving()
    {
        isMoving = true; // Start moving when sanity reaches 25
        Debug.Log("Enemy started moving towards the player.");
    }

    void OnDestroy()
    {
        if (SanityMeter.Instance != null)
        {
            SanityMeter.Instance.OnReached95 -= StartMoving; // Unsubscribe from the event when the enemy is destroyed
        }
    }
}
