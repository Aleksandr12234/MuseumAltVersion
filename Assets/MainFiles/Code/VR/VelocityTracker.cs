using UnityEngine;

public class VelocityTracker : MonoBehaviour
{
    public float Speed { get; private set; }
    public Vector3 Velocity { get; private set; }

    private Vector3 _previousPosition;
    private float _previousTime;

    private void FixedUpdate()
    {
        float currentTime = Time.fixedTime;
        float deltaTime = currentTime - _previousTime;

        if (deltaTime > 0)
        {
            Vector3 currentPosition = transform.position;
            Vector3 displacement = currentPosition - _previousPosition;
            Velocity = displacement / deltaTime;
            Speed = Velocity.magnitude;

            _previousPosition = currentPosition;
            _previousTime = currentTime;
        }
        else
        {
            // Если время не изменилось, сбрасываем скорость
            Velocity = Vector3.zero;
            Speed = 0;
        }
    }

    private void OnEnable()
    {
        _previousPosition = transform.position;
        _previousTime = Time.fixedTime;
    }
}