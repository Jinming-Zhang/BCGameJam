using UnityEngine;

public abstract class PlayerController : MonoBehaviour, IPlayerController
{
    public virtual void DoPowerup(float value)
    {
        throw new System.NotImplementedException();
    }
}