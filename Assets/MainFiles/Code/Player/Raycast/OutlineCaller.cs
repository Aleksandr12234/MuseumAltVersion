using UnityEngine;

public class OutlineCaller: MonoBehaviour
{
    private GameObject _previuGameObject;

    public void Call(GameObject interactiveObject)
    {
        if (_previuGameObject != null && _previuGameObject != interactiveObject && _previuGameObject.TryGetComponent<Outline>(out var component1))
        {
            component1.enabled = false;
        }
        if (interactiveObject != null && interactiveObject.TryGetComponent<Outline>(out var component))
        {
            component.enabled = true;
            _previuGameObject = interactiveObject;
        }
    }
}
