using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
        m_player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, m_player.transform.position.z);
	}

    private GameObject m_player;
}
