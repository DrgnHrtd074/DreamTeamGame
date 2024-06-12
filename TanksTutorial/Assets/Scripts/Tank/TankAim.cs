using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TankAim : MonoBehaviour
{
    public Transform m_Turret;
    private LayerMask m_LayerMask;
    //float scroll = Input.GetAxis("MouseScrollWheel");
    float currentScrollDelta = -11f;

    private void Awake()
    {
        m_LayerMask = LayerMask.GetMask("Ground");
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, m_LayerMask))
        {
            m_Turret.LookAt(hit.point);
        }


        currentScrollDelta += Input.mouseScrollDelta.y;
        Camera.main.orthographicSize = currentScrollDelta * -1;
        if (Camera.main.orthographicSize > 25)
        {
            Camera.main.orthographicSize = 25;
        }
        if (Camera.main.orthographicSize < 5)
        {
            Camera.main.orthographicSize = 5;
        }

        /*if (scroll > 0f) // forward
        {
            Camera.main.orthographicSize = scroll;
        }
        else if (scroll < 0f) // backwards
        {
            Camera.main.orthographicSize = scroll;
        }*/

    }
}
