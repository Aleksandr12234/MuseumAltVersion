using UnityEngine;

public interface IPickableObject
{
    public void MovementToHands(Transform userPosition);
    public void ToggleFreeze();
}
