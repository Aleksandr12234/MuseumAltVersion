using System.Collections.Generic;
using UnityEngine;

public class InteractionRay : MonoBehaviour
{
    [SerializeField] private float _rayLength = 3;
    [SerializeField] private string[] _layersName;

    [SerializeField] private OutlineCaller _outlineCaller;
    [SerializeField] private ActionCaller _actionCaller;
    [SerializeField] private PickupCaller _pickupCaller;
    [SerializeField] private ButtonActionsCaller _buttonActionsCaller;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        GameObject interactiveObject = null;
        int layerMask = LayerMask.GetMask(_layersName);
        if (Physics.Raycast(ray, out RaycastHit hit, _rayLength, layerMask))
        {
            interactiveObject = hit.collider.gameObject;
        }

        if (_outlineCaller != null) _outlineCaller.Call(interactiveObject);
        if (_actionCaller != null) _actionCaller.Call(interactiveObject);
        if (_pickupCaller != null) _pickupCaller.Call(interactiveObject, transform);
        if (_buttonActionsCaller != null) _buttonActionsCaller.Call(interactiveObject);

        //ЛКМ - действие; ПКМ - поднять/опустить

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, transform.forward * _rayLength);
    }
}
