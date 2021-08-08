using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private InputAction x;
    public PlayableDirector director;

    private void OnEnable()
    {
        x.Enable();
    }

    private void OnDisable()
    {
       x.Disable();
    }
    
    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    void Start()
    {
        x.performed += _ => Play();

    }


    void Play()
    {
        director.Play();
    }

}
