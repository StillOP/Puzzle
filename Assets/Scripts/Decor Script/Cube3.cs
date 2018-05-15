using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube3 : MonoBehaviour {

	// Use this for initialization
	void Start () {

        m_cube2 = GameObject.Find("Cube2");
        m_cube2Rigidbody = m_cube2.GetComponent<Rigidbody>();
        m_applyGravity = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.z >= 113.2f && transform.position.z <= 114.8f)
        {
            m_cube2Rigidbody.useGravity = true;
            m_applyGravity = true;
        }
        else
        {
            if(m_applyGravity)
            {
                m_cube2Rigidbody.useGravity = false;
                m_cube2Rigidbody.velocity = new Vector3(m_cube2Rigidbody.velocity.x, 10.0f, m_cube2Rigidbody.velocity.z);
                m_applyGravity = false;
            }
        }
		
	}

    GameObject m_cube2;
    Rigidbody m_cube2Rigidbody;
    bool m_applyGravity;
}