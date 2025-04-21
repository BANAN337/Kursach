using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            CameraRotation();
            MoveCamera();
        }
    }

    private void CameraRotation()
    {
        var mouseY = Input.GetAxis("Mouse X");
        var mouseX = Input.GetAxis("Mouse Y");

        transform.localEulerAngles += new Vector3(-mouseX, mouseY, 0);
    }

    private void MoveCamera()
    {
        var xVelocity = Input.GetAxis("Horizontal");
        var zVelocity = Input.GetAxis("Vertical");

        transform.position += transform.TransformDirection(new Vector3(xVelocity, 0, zVelocity)) * 0.01f;
    }
}