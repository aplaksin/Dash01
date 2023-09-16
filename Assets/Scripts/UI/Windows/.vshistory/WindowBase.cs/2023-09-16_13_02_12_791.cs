
using UnityEngine;
using UnityEngine.UI;


public abstract class WindowBase : MonoBehaviour
{
    [SerializeField] protected Button _closeButton;


    public void Construct()
    {
        
    }

    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        Initialize();
        SubscribeUpdates();
    }

    private void OnDestroy()
    {
        Cleanup();
    }

    protected virtual void OnAwake()
    {
        if(_closeButton != null)
        {
            _closeButton.onClick.AddListener(() => Destroy(gameObject));
        }
            
    }

    protected virtual void Initialize() { }
    protected virtual void SubscribeUpdates() { }
    protected virtual void Cleanup() { }
}
