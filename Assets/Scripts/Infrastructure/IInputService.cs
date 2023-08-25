using static InputService;

public interface IInputService : IService
{
    public void SubscribeOnMoveEvent(OnMoveEvent onMove);
    public void UnsubscribeOnMoveEvent(OnMoveEvent onMove);
}
