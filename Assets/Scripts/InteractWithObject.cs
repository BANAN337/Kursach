using System;
using UnityEngine;

public class InteractWithObject : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private PathHandler pathHandler;
    [SerializeField] private SeekingTarget seekingTarget; 
    [SerializeField] private RunningTarget runningTarget;

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
}
