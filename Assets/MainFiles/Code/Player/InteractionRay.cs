using UnityEngine;

public class InteractionRay : MonoBehaviour
{
    [SerializeField] private float _rayLength = 3;
    private GameObject _selectedObject;
    private GameObject _previuGameObject;

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
            interactiveObject = hit.collider.gameObject;
        }

        //обводка
        if (_previuGameObject != null && _previuGameObject != interactiveObject && _previuGameObject.TryGetComponent<Outline>(out var component1))
        {
            component1.enabled = false;
        }
        if (interactiveObject != null && interactiveObject.TryGetComponent<Outline>(out var component))
        {
            component.enabled = true;
            _previuGameObject = interactiveObject;
        }

        //ЛКМ - действие; ПКМ - поднять/опустить
        if (interactiveObject != null && interactiveObject.TryGetComponent<IUsebleObject>(out var useble))
        {
            if (Input.GetMouseButtonDown(0)) useble.Action();
            else if (Input.GetMouseButton(0)) useble.ActiveAction();
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (_selectedObject != null)
            {
                _selectedObject.GetComponent<IPickableObject>().ToggleFreeze();
                _selectedObject = null;
            }
            else if (interactiveObject!= null && interactiveObject.GetComponent<IPickableObject>() != null)
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
