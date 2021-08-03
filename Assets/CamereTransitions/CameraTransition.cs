using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTransition: MonoBehaviour
{

    [SerializeField] private InputAction action;

    private Animator _animator;
    private int cameraID = 0;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    void Start()
    {
        action.performed += _ => SwitchState();
    }

   
    void Update()
    {
        
    }


    private void SwitchState()
    {
        if (cameraID == 0)
        {
            _animator.Play("onCrashed");
            cameraID = 1;
        }
        else if (cameraID == 1)
        {
            _animator.Play("onBody");
            cameraID = 2;
        }
        else
        {
            _animator.Play("onCrashed");
            cameraID = 1;
        }
    }
}
