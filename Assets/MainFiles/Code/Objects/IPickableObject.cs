using UnityEngine;

public interface IPickableObject
{
    public void MovementToHands(Transform userPosition, float zoom);
    public void ToggleFreeze();
}
