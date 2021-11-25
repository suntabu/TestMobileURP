using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRotate : MonoBehaviour
{
    private Vector3 lastPos, delta;
    private bool isOnModel, isDown;
    private Transform target;

    void Update()
    {
        if (isDown && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo))
        {
            delta = Input.mousePosition - lastPos;
            lastPos = Input.mousePosition;
            if (hitInfo.collider.transform)
                target = hitInfo.collider.transform;
            else
            {
                target = null;
            }
        }
        else
        {
            target = null;
        }

        if (target)
        {
            target.localRotation *= Quaternion.Euler(0, delta.x, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
            isDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDown = false;
        }
    }
}