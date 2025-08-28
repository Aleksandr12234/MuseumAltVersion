using Unity.VisualScripting;
using UnityEngine;

public class Affordance : MonoBehaviour
{

    private Outline _outline;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
    }


    private void OnTriggerEnter(Collider collision)
    {
        //if (collision.collider.CompareTag("AffordanceCaller"))

            _outline.enabled = true;

    }

    private void OnTriggerExit(Collider collision)
    {
        //if (collision.collider.CompareTag("AffordanceCaller"))
        _outline.enabled = false;
    }


}
