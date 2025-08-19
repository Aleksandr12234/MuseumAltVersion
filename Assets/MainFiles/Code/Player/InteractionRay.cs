using UnityEngine;

public class InteractionRay : MonoBehaviour
{
    [SerializeField] private float _rayLength = 3;
    private GameObject _selectedObject;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        GameObject interactiveObject = null;
        int layerMask = LayerMask.GetMask("InteractiveObjects", "StaticInteractiveObjects");

        if (Physics.Raycast(ray, out RaycastHit hit, _rayLength, layerMask))
        {
            Debug.Log("Пересечение с " + hit.collider.gameObject.name);
            interactiveObject = hit.collider.gameObject;
        }
        else
        {
            Debug.Log("Не пересекается");
        }

        //ЛКМ - действие
        if (Input.GetMouseButtonDown(0))
        {
            if (_selectedObject != null && _selectedObject.TryGetComponent<IUsebleObject>(out var useble))
            {
                useble.Action();
            }
            else if (_selectedObject == null && interactiveObject != null && interactiveObject.TryGetComponent<IUsebleObject>(out var use))
            {
                use.Action();
            }
        }
        //ПКМ - поднятие
        else if (Input.GetMouseButtonDown(1))
        {
            if (_selectedObject != null)
            {
                _selectedObject.GetComponent<IPickableObject>().ToggleFreeze();
                _selectedObject = null;
            }
            else if (interactiveObject != null && interactiveObject.GetComponent<IPickableObject>() != null)
            {
                _selectedObject = interactiveObject;
                _selectedObject.GetComponent<IPickableObject>().ToggleFreeze();
            }
        }

        if (_selectedObject != null) _selectedObject.GetComponent<IPickableObject>().MovementToHands(transform);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, transform.forward * _rayLength);
    }
}
