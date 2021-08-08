using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTransition: MonoBehaviour
{

    [SerializeField] private InputAction one;
    [SerializeField] private InputAction two;
    [SerializeField] private InputAction three;
    [SerializeField] private InputAction four;
    [SerializeField] private InputAction five;

    private Animator _animator;
    private int cameraID = 0;
    public bool warndreieck = false;
    public bool body = false;
    public bool car = true;
    public bool kofferraum = true;
    public bool phone = true;
    public GameObject bodyInCar;
    public GameObject bodyOnFloor;
    public GameObject dreieck;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        one.Enable();
        two.Enable();
        three.Enable();
        four.Enable();
        five.Enable();
    }

    private void OnDisable()
    {
        one.Disable();
        two.Disable();
        three.Disable();
        four.Disable();
        five.Disable();
    }

    void Start()
    {
        one.performed += _ => SwitchState(1);
        two.performed += _ => SwitchState(2);
        three.performed += _ => SwitchState(3);
        four.performed += _ => SwitchState(4);
        five.performed += _ => SwitchState(5);
    }

   
    void Update()
    {
        
    }


    private void SwitchState(int input)
    {
        switch (input)
        {
            case 1:
                if (!car) break;
                _animator.Play("onCrashed");
                Debug.Log("Crashed");
                break;
            case 2:
                if (!body) break;
                _animator.Play("onBody");
                Debug.Log("Body");
                break;
            case 3:
                if(!kofferraum) break;
                _animator.Play("onBackOfCar");
                Debug.Log("Kofferraum");
                break;
            case 4:
                if (!warndreieck) break;
                _animator.Play("onWarndreieck");
                Debug.Log("Warndreieck");
                break;
            case 5:
                if (!phone) break;
                _animator.Play("onPhone");
                Debug.Log("Phone");
                break;
        }
        
        
    }
}
