using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateForm : MonoBehaviour {

	// Use this for initialization
	void Start () {

        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.velocity = new Vector3(0.0f, 0.0f, 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.z >= 240.0f) { m_rigidbody.velocity = new Vector3(0.0f, 0.0f, -5.0f); }
        if (transform.position.z <= 180.0f) { m_rigidbody.velocity = new Vector3(0.0f, 0.0f, 5.0f); }

    }

    Rigidbody m_rigidbody;
}