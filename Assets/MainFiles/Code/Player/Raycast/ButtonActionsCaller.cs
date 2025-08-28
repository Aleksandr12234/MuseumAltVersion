using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonActionsCaller : MonoBehaviour
{
    private Button _previuButton;
    private GameObject _myEventSystem;

    public void Start()
    {
        _myEventSystem = GameObject.Find("EventSystem");
    }

    public void Call(GameObject interactiveObject)
    {
        Button button = null;
        if (interactiveObject != null && interactiveObject.TryGetComponent<Button>(out button))
        {
            _myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(interactiveObject);
            _previuButton = button;
            if (Input.GetMouseButtonDown(0)) button.onClick.Invoke();
        }
        else if (_previuButton != button) _myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
    }
}
