using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject Target;

    public float m_Distance = 200;

    public float m_CameraSpeed = 100.0f;

    private Camera m_Camera;

    private int m_ShitStainMask;

    private GameCanvasManager m_GameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GetComponent<Camera>();
        m_ShitStainMask = LayerMask.GetMask("ShitStain");
        m_GameManager = GameCanvasManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        float l_deltaTime = Time.deltaTime;

        if (m_GameManager.GetState() == GameStates.PLAY)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 l_MousePosition = Input.mousePosition;

                Ray l_Ray = m_Camera.ScreenPointToRay(l_MousePosition);
                RaycastHit l_Hit;
                if (Physics.Raycast(l_Ray, out l_Hit, 200, m_ShitStainMask, QueryTriggerInteraction.Collide))
                {
                    m_GameManager.IncreaseScore(1);
                    m_GameManager.IncreaseMess(-1);
                    Destroy(l_Hit.collider.gameObject);
                }
                //Debug.Log("Drawing Line" + l_Ray);
                //Debug.DrawRay(l_Ray.origin, l_Ray.direction * m_Distance);

            }
        }


        transform.position = transform.position + Input.GetAxis("Horizontal") * transform.right * m_CameraSpeed * l_deltaTime;
        transform.position = transform.position + Input.GetAxis("Vertical") * transform.up * m_CameraSpeed * l_deltaTime;
        transform.LookAt(Target.transform);

    }
}
