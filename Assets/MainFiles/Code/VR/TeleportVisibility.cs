using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class TeleportVisibility : MonoBehaviour
{
    [Header("Input Settings")]
    public InputActionReference teleportAction;

    [Header("Prefabs to Control")]
    public GameObject[] prefabsToShow;

    [Header("Delay Settings")]
    public float disableDelay = 0.5f; // Задержка перед выключением

    private Coroutine disableCoroutine;
    private bool isTeleportActive;

    private void OnEnable()
    {
        // Подписываемся на события действия
        if (teleportAction != null)
        {
            teleportAction.action.started += OnTeleportAction;
            teleportAction.action.canceled += OnTeleportAction;
            teleportAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        // Отписываемся от событий
        if (teleportAction != null)
        {
            teleportAction.action.started -= OnTeleportAction;
            teleportAction.action.canceled -= OnTeleportAction;
        }

        // Останавливаем корутину, если она активна
        if (disableCoroutine != null)
        {
            StopCoroutine(disableCoroutine);
            disableCoroutine = null;
        }
    }

    private void OnTeleportAction(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            // Отменяем запланированное выключение, если оно есть
            if (disableCoroutine != null)
            {
                StopCoroutine(disableCoroutine);
                disableCoroutine = null;
            }

            // Включаем префабы
            SetPrefabsState(true);
            isTeleportActive = true;
        }
        else if (context.phase == InputActionPhase.Canceled && isTeleportActive)
        {
            // Запускаем корутину для выключения с задержкой
            disableCoroutine = StartCoroutine(DisableAfterDelay());
        }
    }

    private IEnumerator DisableAfterDelay()
    {
        // Ждем указанное время
        yield return new WaitForSeconds(disableDelay);

        // Выключаем префабы
        SetPrefabsState(false);
        isTeleportActive = false;
        disableCoroutine = null;
    }

    private void SetPrefabsState(bool state)
    {
        foreach (var prefab in prefabsToShow)
        {
            if (prefab != null)
                prefab.SetActive(state);
        }
    }
}