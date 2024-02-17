using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractable : MonoBehaviour
{
    [SerializeField] private float attractableRadius = 0.5f;
    [SerializeField] private float attractionStrength = 1f;
    [SerializeField] private LayerMask blackHoleLayer;
    [SerializeField] GameObject attractPoint;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        attractPoint = GameObject.FindGameObjectWithTag("Attract Point");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, attractPoint.transform.position) <= 0.2 && gameObject.tag == "Enemy")
        {
            if(GetComponent<GruntEnemy>() != null)
            {
                GetComponent<GruntEnemy>().TakeDamage(1, new Vector2(0, 0));
            }

            if (GetComponent<RangerEnemy>() != null)
            {
                GetComponent<RangerEnemy>().TakeDamage(1, new Vector2(0, 0));
            }
        }
    }

    private void FixedUpdate()
    {
        if (attractPoint == null || !attractPoint.GetComponent<AttractPoint>().isOn) return; // do nothing if no attract point


        float magsqr; // offset squared between object and attract point
        Vector3 offset; // distance to attract point

        offset = attractPoint.transform.position - transform.position;
        offset.z = 0; // because 2D

        magsqr = offset.sqrMagnitude;

        // prevent division by 0
        if (magsqr > 0.0001f)
        {
            rb.AddForce((attractionStrength * offset.normalized) * rb.mass);
        }
    }
}
