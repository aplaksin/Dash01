﻿using System;
using UnityEngine;
using UnityEngine.UI;


public class OpenWindowButton : MonoBehaviour
{
    public Button Button;
    public WindowId WindowId;
    private IWindowService _windowService;

    public void Init(IWindowService windowService) {
        _windowService = windowService;
    }
      

    private void Awake() {
        Button.onClick.AddListener(Open);
    }

    private void Open()
    {
        _windowService.OpenWindowById(WindowId);
    }
     
}
