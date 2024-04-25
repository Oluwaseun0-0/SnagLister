using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private RoundedBoxProperties _buttonColour;

    private Color _defaultColor;

    private Color _onColor;

    private void Awake()
    {
        _buttonColour = GetComponentInChildren<RoundedBoxProperties>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _defaultColor = _buttonColour.Color;
       // _onColor = new Color(0.46f, 0.96f, 0, 0.15f);
        _onColor = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleColour()
    {
        Debug.Log("I am Toggling color");
        if ( _buttonColour.Color == _defaultColor)
        {
            _buttonColour.Color = _onColor;
            
        }
        else
        {
            _buttonColour.Color = _defaultColor;
        }
    }
}
