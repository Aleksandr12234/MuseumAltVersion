using UnityEngine;

public class PickupCaller:MonoBehaviour
{
    [SerializeField] private float _zoom = 0.5f;
    private GameObject _selectedObject;

    public void Call(GameObject interactiveObject, Transform transform)
    { 
        if (Input.GetMouseButtonDown(1))
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

        if (Input.mouseScrollDelta.y>0 && _zoom<0.7) { _zoom += 0.05f; }
        else if(Input.mouseScrollDelta.y<0 && _zoom>0.2){_zoom -= 0.05f;}

        if (_selectedObject != null) _selectedObject.GetComponent<IPickableObject>().MovementToHands(transform, _zoom);
    }
}
