using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CollisionType { Entered, Left, Stay }

public struct CollisionDetails
{
    public Collision m_collision;
    public CollisionType m_type;
    public bool m_collideOnX;
    //public bool m_collideOnY;

    public CollisionDetails(Collision l_collision, CollisionType l_type, bool l_collideOnX)
    {
        m_collision = l_collision;
        m_type = l_type;
        m_collideOnX = l_collideOnX;
    }
}

public class CollisionHandler {

    public CollisionHandler(string l_name)
    {
        m_player = GameObject.Find("Player");
        m_isPushing = false;
    }

    public void HandleCollision()
    {
        if(m_isPushing && !Input.GetKey(KeyCode.A)) { m_isPushing = false; }
    }

    public KeyValuePair<bool, Collider> CollideAPushable(Direction l_direction)
    {
        RaycastHit hit;
        float max = m_player.GetComponent<BoxCollider>().bounds.extents.z + 0.1f;
        Vector3 direction = l_direction == Direction.Right ? Vector3.forward : Vector3.back;

        if (Physics.Raycast(m_player.transform.position, direction, out hit, max))
        {
            if(hit.transform.tag == "Moving Cube")
            {
                return new KeyValuePair<bool, Collider>(true, hit.collider);
            }
            return new KeyValuePair<bool, Collider>(false, null);
        }

        return new KeyValuePair<bool, Collider>(false, null);
    }

    public void SetIsPushing(bool l_isPushing) { m_isPushing = l_isPushing; }
    public bool IsPushing() { return m_isPushing; }


    private GameObject m_player;
    private bool m_isPushing;
}
