using UnityEngine;

public abstract class PlayerController : MonoBehaviour, IPlayerController
{
    public virtual void DoPowerup(int value)
    {
        throw new System.NotImplementedException();
    }
}