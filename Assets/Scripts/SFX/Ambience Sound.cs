using UnityEngine;

public class AmbienceSound : MonoBehaviour
{
    public Collider Area;
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        Vector3 closedPoint = Area.ClosestPoint(Player.transform.position);

        transform.position = closedPoint;

    }
}
