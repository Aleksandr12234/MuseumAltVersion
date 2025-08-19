using UnityEngine;

public class PickableObject : MonoBehaviour, IPickableObject
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float x, y, z;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public void MovementToHands(Transform userPosition)
    {
        Vector3 position = userPosition.position + userPosition.forward * 0.5f;

        transform.position = position;
        transform.rotation = userPosition.rotation;
        transform.Rotate(x, y, z);
    }

    public void ToggleFreeze()
    {

        if (!_rigidbody.isKinematic)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.detectCollisions = false;
        }
        else
        {
            _rigidbody.isKinematic = false;
            _rigidbody.detectCollisions = true;
        }
    }
}
