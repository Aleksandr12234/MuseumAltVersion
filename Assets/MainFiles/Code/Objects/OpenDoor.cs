using UnityEngine;

public class OpenDoor : MonoBehaviour, IUsebleObject
{
    [SerializeField] private Animator _DoorAnimator;

    public void Action()
    {
        if (_DoorAnimator.GetBool("isOpen")) _DoorAnimator.SetBool("isOpen", false);
        else _DoorAnimator.SetBool("isOpen", true);
    }
}
