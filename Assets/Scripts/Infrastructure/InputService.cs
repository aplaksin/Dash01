using UnityEngine;

public abstract class InputService : IInputService
{
    public delegate void OnMoveEvent(Vector2 position);
    public event OnMoveEvent OnMove;

    public void SubscribeOnMoveEvent(OnMoveEvent onMove)
    {
        OnMove += onMove;
    }

    public void UnsubscribeOnMoveEvent(OnMoveEvent onMove)
    {
        OnMove -= onMove;
    }

    protected virtual void InvokeOnMove(Vector2 direction)
    {
        OnMove?.Invoke(direction);
    }
}