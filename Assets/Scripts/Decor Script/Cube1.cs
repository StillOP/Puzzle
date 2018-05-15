using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube1 : MonoBehaviour {

	// Use this for initialization
	void Start () {

        m_door = GameObject.Find("Door1");
        m_doorAnimator = m_door.GetComponentInChildren<Animator>();
        m_open = false;
	}
	
	// Update is called once per frame
	void Update () {

        if(transform.position.z <= 66.0f && transform.position.z >= 65.0f)
        {
            if(!m_open) { m_doorAnimator.Play("door_2_open"); m_open = true; }
        }
        else
        {
            if(m_open)
            {
                m_doorAnimator.Play("door_2_close"); m_open = false;
            }
        }
	}

    GameObject m_door;
    Animator m_doorAnimator;
    bool m_open;
}