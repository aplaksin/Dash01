
public interface IWindowService : IService
{
    void OpenWindowById(WindowId windowId);
    void OpenWindowById(WindowId windowId, int score);
}
