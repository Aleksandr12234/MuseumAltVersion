using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private StepSound _audioScript;

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.TransformDirection(new Vector3(moveX, 0, moveZ)) * _speed;
        moveDirection.y = _rigidbody.linearVelocity.y;
        _rigidbody.linearVelocity = moveDirection;
        if (_audioScript != null && (moveX != 0 || moveZ != 0)) _audioScript.Step();
    }
}
