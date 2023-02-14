using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float cameraSpeed = 10f;
    [SerializeField] private float cameraRotationSpeed = 100f;
    [SerializeField] private float cameraZoomSpeed = 10f;
    [SerializeField] private float cameraPivotSpeed = 10f;
    [SerializeField] private float speedUpFactor = 2f; 
    private float _currentSpeedUp = 1f;
    
    private PlayerInput _playerInput;
    private InputAction _moveCameraAction;
    private InputAction _rotateCameraAction;
    private InputAction _zoomCamera;
    private InputAction _enableMovement;
    private InputAction _speedUp;
    private InputAction _middleMouse;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        _moveCameraAction = _playerInput.actions["MoveCamera"];
        _rotateCameraAction = _playerInput.actions["RotateCamera"];
        _zoomCamera = _playerInput.actions["ZoomCamera"];
        _enableMovement = _playerInput.actions["EnableMovement"];
        _speedUp = _playerInput.actions["Speedup"];
        _middleMouse = _playerInput.actions["MiddleMouseButton"];
        
        _moveCameraAction.Enable();
        _rotateCameraAction.Enable();
        _zoomCamera.Enable();
        _enableMovement.Enable();
        _speedUp.Enable();
        _middleMouse.Enable();
    }
    
    private void OnDisable()
    {
        _moveCameraAction.Disable();
        _rotateCameraAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (_enableMovement.IsPressed())
        {
            MoveCamera(_moveCameraAction.ReadValue<Vector3>());
            RotateCamera();
            
            // Get the value of the scroll wheel
            
        }
        if(_middleMouse.IsPressed())
            PivotCamera();
        
        ZoomCamera(_zoomCamera.ReadValue<float>());
    }

    void MoveCamera(Vector3 direction)
    {
        var relative = new Vector3();
        // Adjusts the direction of the camera movement based on the camera's rotation`
        if(direction.z > 0) relative += mainCamera.transform.forward;
        else if(direction.z < 0) relative -= mainCamera.transform.forward;
        if(direction.x > 0) relative += mainCamera.transform.right;
        else if(direction.x < 0) relative -= mainCamera.transform.right;
        relative.y += direction.y;
        mainCamera.transform.position += relative * (Time.deltaTime * cameraSpeed * _currentSpeedUp);
    }

    void PivotCamera()
    {
        // Pivots the camera around the origin
        mainCamera.transform.RotateAround(Vector3.zero, Vector3.up,
            Mouse.current.delta.ReadValue().x * Time.deltaTime * cameraPivotSpeed * _currentSpeedUp);
        mainCamera.transform.RotateAround(Vector3.zero, Vector3.right, Mouse.current.delta.ReadValue().y * Time.deltaTime * cameraPivotSpeed * _currentSpeedUp);
    }

    void ZoomCamera(float amount)
    {
        mainCamera.transform.position += mainCamera.transform.forward * (amount * Time.deltaTime * cameraZoomSpeed * _currentSpeedUp);
    }

    void RotateCamera()
    {
        var mousePos = Mouse.current.delta.ReadValue();
        mainCamera.transform.Rotate(Vector3.up * (mousePos.x  * Time.deltaTime * cameraRotationSpeed * _currentSpeedUp), Space.Self);
        mainCamera.transform.Rotate(Vector3.left * (mousePos.y * Time.deltaTime * cameraRotationSpeed * _currentSpeedUp), Space.Self);
    }
}