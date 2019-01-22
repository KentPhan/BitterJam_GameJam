using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitExplosionScript : MonoBehaviour
{

    [SerializeField] private GameObject m_ShitStainPrefab;

    private ParticleSystem m_ParticleSystem;
    public Transform ShitParent;

    private ParticleCollisionEvent[] m_CollisionEvents;

    private GameCanvasManager m_GameManager;

    private void Awake()
    {
        m_ParticleSystem = GetComponent<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_CollisionEvents = new ParticleCollisionEvent[20];
        m_GameManager = GameCanvasManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shard()
    {
        m_ParticleSystem.Play();
    }

    public void OnParticleCollision(GameObject other)
    {
        int collCount = m_ParticleSystem.GetSafeCollisionEventSize();

        if (collCount > m_CollisionEvents.Length)
            m_CollisionEvents = new ParticleCollisionEvent[collCount];

        int eventCount = m_ParticleSystem.GetCollisionEvents(other, m_CollisionEvents);

        for (int i = 0; i < eventCount; i++)
        {
            //Debug.Log(other.tag);
            m_GameManager.IncreaseMess(1);
            Instantiate(m_ShitStainPrefab, m_CollisionEvents[i].intersection, Quaternion.AngleAxis(90, Vector3.up) * Quaternion.LookRotation(m_CollisionEvents[i].normal, m_CollisionEvents[i].normal), ShitParent);

            //TODO: Do your collision stuff here. 
            // You can access the CollisionEvent[i] to obtain point of intersection, normals that kind of thing
            // You can simply use "other" GameObject to access it's rigidbody to apply force, or check if it implements a class that takes damage or whatever
        }
    }
}
