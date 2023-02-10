using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float cameraSpeed = 10f;
    [SerializeField] private float cameraRotationSpeed = 100f;
    [SerializeField] private float cameraZoomSpeed = 10f;
    [SerializeField] private float cameraPivotSpeed = 10f;
    [SerializeField] private float speedUpFactor = 2f;
    private float _currentSpeedUp = 1f;
    
    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.LeftShift))
            _currentSpeedUp = speedUpFactor;
        else
            _currentSpeedUp = 1f;
        
        // Move the camera with WASD
        if (Input.GetKey(KeyCode.W))
            MoveCamera(transform.forward);
        
        
        if (Input.GetKey(KeyCode.S))
            MoveCamera(-transform.forward);
        
        
        if (Input.GetKey(KeyCode.A))
            MoveCamera(-transform.right);
        
        
        if (Input.GetKey(KeyCode.D))
            MoveCamera(transform.right);
        
        
        if (Input.GetKey(KeyCode.Q))
            MoveCamera(Vector3.down);
        
        
        if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space))
            MoveCamera(Vector3.up);
        
        
        // Rotate camera with mouse movement inverted
        if (Input.GetMouseButton(1))
        {
            RotateCamera();
        }
        
        // Zoom in and out with mouse wheel
        if (Input.mouseScrollDelta.y != 0)
        {
            ZoomCamera();
        }
        
        // Pivot around 0,0 with middle mouse button
        if (Input.GetMouseButton(2))
        {
            PivotCamera();
        }
    }

    void MoveCamera(Vector3 direction)
    {
        mainCamera.transform.position += direction * (Time.deltaTime * cameraSpeed * _currentSpeedUp);
    }

    void PivotCamera()
    {
        // Pivots the camera around the origin
        mainCamera.transform.RotateAround(Vector3.zero, Vector3.up,
            Input.GetAxis("Mouse X") * Time.deltaTime * cameraPivotSpeed * _currentSpeedUp);
        mainCamera.transform.RotateAround(Vector3.zero, Vector3.right, Input.GetAxis("Mouse Y") * Time.deltaTime * cameraPivotSpeed * _currentSpeedUp);
    }

    void ZoomCamera()
    {
        mainCamera.transform.position += mainCamera.transform.forward * (Input.mouseScrollDelta.y * Time.deltaTime * cameraZoomSpeed * _currentSpeedUp);
    }

    void RotateCamera()
    {
        mainCamera.transform.Rotate(Vector3.up * (Input.GetAxis("Mouse X") * Time.deltaTime * cameraRotationSpeed * _currentSpeedUp), Space.World);
        mainCamera.transform.Rotate(Vector3.left * (Input.GetAxis("Mouse Y") * Time.deltaTime * cameraRotationSpeed * _currentSpeedUp), Space.Self);
    }
}