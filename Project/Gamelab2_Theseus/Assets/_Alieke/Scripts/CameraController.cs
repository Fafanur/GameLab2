//made by Alieke
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    Vector2 mouseChange;
    Vector2 smoothAmount;
    public float mouseSensitivity;
    public float smoothing;
    public bool maymoveMouse;

    void FixedUpdate () {
        MoveCamera();
	}

    void MoveCamera()
    {
        if (maymoveMouse)
        {
            Vector2 mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            mouseMovement = Vector2.Scale(mouseMovement, new Vector2(mouseSensitivity * smoothing, mouseSensitivity * smoothing));

            smoothAmount.x = Mathf.Lerp(smoothAmount.x, mouseMovement.x, 1f / smoothing);
            smoothAmount.y = Mathf.Lerp(smoothAmount.y, mouseMovement.y, 1f / smoothing);

            mouseChange += smoothAmount;
            mouseChange.y = Mathf.Clamp(mouseChange.y, -65, 65);

            transform.localRotation = Quaternion.AngleAxis(-mouseChange.y, Vector3.right);
            player.transform.localRotation = Quaternion.AngleAxis(mouseChange.x, player.transform.up);
        }
    }
}
