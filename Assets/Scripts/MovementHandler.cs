using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Left, Right };

public class MovementHandler {

    public MovementHandler(string l_name, float l_speed, float l_jumpSpeed)
    {
        m_player = GameObject.Find(l_name);
        m_rigidbody = m_player.GetComponent<Rigidbody>();

        m_speed = l_speed;
        m_jumpSpeed = l_jumpSpeed;
        m_deceleration = 0.55f;

        m_direction = Direction.Right;
        m_directionChanged = false;
    }

    public void HandleMovement()
    {
        m_rigidbody.AddForce(new Vector3(0.0f, -110.0f, 0.0f), ForceMode.Acceleration);

        if(m_directionChanged)
        {
            int angle = m_direction == Direction.Right ? 0 : 180;
            m_player.transform.rotation = new Quaternion(0, angle, 0, m_player.transform.rotation.w);
            m_directionChanged = false;
        }

        Decelerate();

        if (Mathf.Abs(m_rigidbody.velocity.z) < 0.05f) { m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, m_rigidbody.velocity.y, 0.0f); }
    }

    public void Move(Direction l_direction, bool l_isPushing, KeyValuePair<bool, Collider> l_collision)
    {
        if(l_isPushing && l_collision.Key && l_collision.Value != null)
        {
            Rigidbody rigidbody = l_collision.Value.transform.GetComponent<Rigidbody>();
            float directionCheck = rigidbody.position.z - m_player.transform.position.z;
            if((directionCheck > 0 && l_direction != Direction.Right) || (directionCheck < 0 && l_direction != Direction.Left)) { return; }

            float pushSpeed = l_direction == Direction.Right ? 2.0f : -2.0f;

            rigidbody.transform.position += new Vector3(0.0f, 0.0f,  pushSpeed * Time.deltaTime);
            m_player.transform.position += new Vector3(0.0f, 0.0f, pushSpeed * Time.deltaTime);

            return;
        }

        SetDirection(l_direction);
        if(CollideOnX()) { if(!m_directionChanged) { return; } }

        float speed = l_direction == Direction.Right ? m_speed : -m_speed;

        m_rigidbody.AddForce(new Vector3(0, 0, speed));
    }

    public void Jump()
    {
        if(IsOnTheGround())
        {
            m_rigidbody.AddForce(new Vector3(0, m_jumpSpeed, 0), ForceMode.Impulse);
        }
    }

    public void Decelerate()
    {
        Vector2 friction;
        Vector3 velocity = m_rigidbody.velocity;

        friction.x = m_deceleration * velocity.z;

        if (Mathf.Abs(velocity.z) - Mathf.Abs(friction.x) < 0.0f) { m_rigidbody.velocity = new Vector3(velocity.x, velocity.y, 0.0f); }

        if (velocity.z != 0)
        {
            m_rigidbody.velocity = new Vector3(velocity.x, velocity.y, velocity.z - friction.x);
        }
    }

    public void SetVelocityLimits()
    {
        if(Mathf.Abs(m_rigidbody.velocity.z) > 4.0f)
        {
            float limit = m_rigidbody.velocity.z > 0 ? 4.0f : -4.0f;
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, m_rigidbody.velocity.y, limit);
        }
    }

    public bool IsOnTheGround()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        Vector3 rayOrigin = m_player.GetComponent<BoxCollider>().bounds.center;

        float rayDistance = m_player.GetComponent<BoxCollider>().bounds.extents.y;
        Ray ray = new Ray
        {
            origin = rayOrigin,
            direction = Vector3.down
        };

        if (Physics.Raycast(ray, rayDistance, layerMask)) { return true; }
        return false;
    }

    public bool CollideOnX()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        Vector3 direction = m_direction == Direction.Right ? Vector3.forward : Vector3.back;
        float distance = m_player.GetComponent<BoxCollider>().bounds.extents.z + 0.1f;

        if (Physics.Raycast(m_player.transform.position, direction, distance, layerMask)) { return true; }
        return false;
    }

    public void SetDirection(Direction l_direction)
    {
        if (m_direction == l_direction) { return; }
        m_direction = l_direction;
        m_directionChanged = true;
    }
    public Direction GetDirection() { return m_direction; }
    public bool GetDirectionChanged() { return m_directionChanged; }

    public void SetVelocity(Vector3 l_velocity) { m_rigidbody.velocity = l_velocity; }
    public Vector3 GetVelocity() { return m_rigidbody.velocity;  }




    private GameObject m_player;
    private Rigidbody m_rigidbody;

    private float m_speed;
    private float m_jumpSpeed;
    private float m_deceleration;

    private Direction m_direction;
    private bool m_directionChanged;
}