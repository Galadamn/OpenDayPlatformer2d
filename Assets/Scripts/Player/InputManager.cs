using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;

    public float Horizontal;
    public bool Jump; //JumpHeld;
    public bool DashSprint;
    public bool Attack;
    public bool Interact;

    private void Update()
    {
        Horizontal = 
            _inputSystem.Player.Move.
                ReadValue<Vector2>().x;
        
        Jump = _inputSystem.Player.Jump.
            WasPressedThisFrame();
       /* JumpHeld = _inputSystem.Player.Jump.
            IsPressed();*/
        DashSprint = _inputSystem.Player.Sprint.
            WasPressedThisFrame();
    }

    private void Awake() { _inputSystem = new  InputSystem_Actions(); } 
    private void OnEnable() { _inputSystem.Enable(); }
    private void OnDisable() { _inputSystem.Disable(); } 
}
