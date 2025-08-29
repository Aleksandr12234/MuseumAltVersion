using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Input Settings")]
    public InputActionReference teleportAction;

    [Header("Prefabs to Control")]
    public GameObject[] prefabsToShow;

    private Coroutine disableCoroutine;
    private bool isMenuVisible;

    private void OnEnable()
    {
        // ������������� �� ������� ��������
        if (teleportAction != null)
        {
            teleportAction.action.started += OnMenuAction;
            teleportAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        // ������������ �� �������
        if (teleportAction != null)
        {
            teleportAction.action.started -= OnMenuAction;
        }
    }

    private void OnMenuAction(InputAction.CallbackContext context)
    {
        if (isMenuVisible) {
        // �������� �������
              SetPrefabsState(false);
              isMenuVisible = false;
        }
        else
        {
            SetPrefabsState(true);
            isMenuVisible = true;
        }
    }

    private void SetPrefabsState(bool state)
    {
        foreach (var prefab in prefabsToShow)
        {
            if (prefab != null)
                prefab.SetActive(state);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
