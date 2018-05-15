using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube5 : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

        m_cube4 = GameObject.Find("Cube4");
        m_cube4Rigidbody = m_cube4.GetComponent<Rigidbody>();
        m_applyGravity = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z >= 209.0f && transform.position.z <= 210.0f)
        {
            m_cube4Rigidbody.useGravity = true;
            m_applyGravity = true;
        }
        else
        {
            if (m_applyGravity)
            {
                m_cube4Rigidbody.useGravity = false;
                m_cube4Rigidbody.velocity = new Vector3(m_cube4Rigidbody.velocity.x, 10.0f, m_cube4Rigidbody.velocity.z);
                m_applyGravity = false;
            }
        }

    }

    GameObject m_cube4;
    Rigidbody m_cube4Rigidbody;
    bool m_applyGravity;
}