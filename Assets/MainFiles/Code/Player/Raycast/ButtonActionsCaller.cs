using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonActionsCaller : MonoBehaviour
{
    [SerializeField] private float _responceTime;
    private float _pastTime;
    private Button _previuButton;
    private GameObject _myEventSystem;

    public void Start()
    {
        _myEventSystem = GameObject.Find("EventSystem");
    }

    public void Call(GameObject interactiveObject)
    {
        _pastTime += Time.deltaTime;
        Button button = null;
        if (interactiveObject != null && interactiveObject.TryGetComponent<Button>(out button))
        {
            _myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(interactiveObject);
            _previuButton = button;
            if (Input.GetMouseButton(0) && _pastTime >= _responceTime)
            {
                button.onClick.Invoke();
                _pastTime = 0;
            }
        }
            else if (_previuButton != button) _myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
    }
}
