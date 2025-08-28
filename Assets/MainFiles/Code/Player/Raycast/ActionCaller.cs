using UnityEngine;

public class ActionCaller: MonoBehaviour
{
    public void Call(GameObject interactiveObject)
    {
        if (interactiveObject != null && interactiveObject.TryGetComponent<IUsebleObject>(out var useble))
        {
            if (Input.GetMouseButtonDown(0)) useble.Action();
            else if (Input.GetMouseButton(0)) useble.ActiveAction();
        }
    }
}
