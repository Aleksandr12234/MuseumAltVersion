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
    public float disableDelay = 0.5f; // �������� ����� �����������

    private Coroutine disableCoroutine;
    private bool isTeleportActive;

    private void OnEnable()
    {
        // ������������� �� ������� ��������
        if (teleportAction != null)
        {
            teleportAction.action.started += OnTeleportAction;
            teleportAction.action.canceled += OnTeleportAction;
            teleportAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        // ������������ �� �������
        if (teleportAction != null)
        {
            teleportAction.action.started -= OnTeleportAction;
            teleportAction.action.canceled -= OnTeleportAction;
        }

        // ������������� ��������, ���� ��� �������
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
            // �������� ��������������� ����������, ���� ��� ����
            if (disableCoroutine != null)
            {
                StopCoroutine(disableCoroutine);
                disableCoroutine = null;
            }

            // �������� �������
            SetPrefabsState(true);
            isTeleportActive = true;
        }
        else if (context.phase == InputActionPhase.Canceled && isTeleportActive)
        {
            // ��������� �������� ��� ���������� � ���������
            disableCoroutine = StartCoroutine(DisableAfterDelay());
        }
    }

    private IEnumerator DisableAfterDelay()
    {
        // ���� ��������� �����
        yield return new WaitForSeconds(disableDelay);

        // ��������� �������
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