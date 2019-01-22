using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitStainScript : MonoBehaviour
{
    [SerializeField] private float MinScale = 0.01f;
    [SerializeField] private float MaxScale = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        float scale = Random.Range(MinScale, MaxScale);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
