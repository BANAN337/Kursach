using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[ExcludeFromCodeCoverage]
public class InteractWithObject : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private PathHandler pathHandler;
    [SerializeField] private SeekingTarget seekingTarget;
    [SerializeField] private RunningTarget runningTarget;
    [SerializeField] private Camera[] cameras;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            SelectPoint(DisablePoint);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            SelectPoint(SetStartingPoint);
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            SelectPoint(SetEndPoint);
        }

        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            DisableCameras(0);
        }

        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            DisableCameras(1);
        }

        if (Input.GetKeyUp(KeyCode.Alpha6))
        {
            DisableCameras(2);
        }

        if (Input.GetKeyUp(KeyCode.Alpha7))
        {
            DisableCameras(3);
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            seekingTarget.canMove = true;
            runningTarget.canMove = true;
            seekingTarget.StartMovement();
            runningTarget.StartMovement();
        }
    }

    private void SelectPoint(Action<Point> onPointSelected)
    {
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            if (hit.transform.TryGetComponent(out Point selectedPoint))
            {
                onPointSelected.Invoke(selectedPoint);
            }
        }
    }

    private void DisablePoint(Point selectedPoint)
    {
        selectedPoint.IsNotValid = !selectedPoint.IsNotValid;
    }

    private void SetStartingPoint(Point selectedPoint)
    {
        seekingTarget.transform.position = selectedPoint.transform.position;
    }

    private void SetEndPoint(Point selectedPoint)
    {
        runningTarget.transform.position = selectedPoint.transform.position;
    }

    private void DisableCameras(int cameraToEnable)
    {
        foreach (var cam in cameras)
        {
            if (cam == cameras[cameraToEnable])
            {
                cam.gameObject.SetActive(true);
                continue;
            }

            cam.gameObject.SetActive(false);
        }
    }
}