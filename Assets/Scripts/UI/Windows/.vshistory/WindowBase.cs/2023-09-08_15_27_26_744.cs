﻿
using UnityEngine;
using UnityEngine.UI;


public abstract class WindowBase : MonoBehaviour
{
    [SerializeField] private Button CloseButton;


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
        CloseButton.onClick.AddListener(() => Destroy(gameObject));
    }

    protected virtual void Initialize() { }
    protected virtual void SubscribeUpdates() { }
    protected virtual void Cleanup() { }
}
