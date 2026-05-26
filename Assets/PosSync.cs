using UnityEngine;

public class PosSync : MonoBehaviour
{
    // This script is intended to synchronize the position of the attached GameObject with another GameObject specified in the _gameObject variable


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject _gameObject;
    public Transform _transform;


    void Start()
    {
        // Check if the _gameObject variable has been assigned in the Inspector
        if (_gameObject == null)
        {
            Debug.LogError("PosSync: No GameObject assigned to _gameObject. Please assign a GameObject in the Inspector.");
            return; // Exit Start if no GameObject is assigned
        }
        // Synchronize the position of this GameObject with the position of the specified _gameObject
        _transform.position = _gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (_gameObject != null && _transform != null)
        {
            // Continuously synchronize the position of this GameObject with the position of the specified _gameObject
            //_transform.position = _gameObject.transform.position;
            _transform.rotation = _gameObject.transform.rotation * Quaternion.Euler(180, 180, 0);


        }
    }
}
