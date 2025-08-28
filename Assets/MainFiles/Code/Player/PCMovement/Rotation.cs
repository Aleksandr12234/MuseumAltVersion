using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private int _horizontalSensitivity=50;
    [SerializeField] private int _verticalSensitivity = 100;
    [SerializeField] private int _maxXRotation = 90;

    private float _angle = 0;

    private void Update()
    {
        Vector3 rotateDirection = new Vector3(Input.GetAxis("Mouse X") * _horizontalSensitivity, Input.GetAxis("Mouse Y") * _verticalSensitivity, 0);
        float mouseX = rotateDirection.x * Time.deltaTime;
        float mouseY = rotateDirection.y * Time.deltaTime;
        _angle -= mouseY;
        _angle = Mathf.Clamp(_angle, -_maxXRotation, _maxXRotation);
        transform.Rotate(0, mouseX, 0);
        _camera.localEulerAngles = new Vector3(_angle, 0, 0);
    }
}
