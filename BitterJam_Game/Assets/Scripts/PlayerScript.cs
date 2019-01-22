using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject Target;

    public float m_Distance = 200;

    private Camera m_Camera;

    private int m_ShitStainMask;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GetComponent<Camera>();
        m_ShitStainMask = LayerMask.GetMask("ShitStain");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {

            Vector3 l_MousePosition = Input.mousePosition;

            Ray l_Ray = m_Camera.ScreenPointToRay(l_MousePosition);
            RaycastHit l_Hit;
            if (Physics.Raycast(l_Ray, out l_Hit, 200, m_ShitStainMask, QueryTriggerInteraction.Collide))
            {
                Destroy(l_Hit.collider.gameObject);
            }
            Debug.Log("Drawing Line" + l_Ray);

            Debug.DrawRay(l_Ray.origin, l_Ray.direction * m_Distance);

        }
    }
}
