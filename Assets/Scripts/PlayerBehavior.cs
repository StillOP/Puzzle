using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        m_inputHandler = new InputHandler();
        m_movementHandler = new MovementHandler("Player", m_speed, m_jumpSpeed);
        m_animationHandler = new AnimationHandler("Player");
        m_collisionHandler = new CollisionHandler("Player");

        m_inputHandler.BindInput("MoveRight", KeyCode.RightArrow, InputType.Default, Controls);
        m_inputHandler.BindInput("MoveLeft", KeyCode.LeftArrow, InputType.Default, Controls);
        m_inputHandler.BindInput("Jump", KeyCode.Space, InputType.KeyDown, Controls);
        m_inputHandler.BindInput("Push", KeyCode.A, InputType.KeyDown, Pull);
        m_inputHandler.BindInput("Let", KeyCode.A, InputType.KeyUp, Pull);

        m_animationHandler.AddAnimation(AnimationType.Idle, "Idle");
        m_animationHandler.AddAnimation(AnimationType.Run, "Fast Run");
        m_animationHandler.AddAnimation(AnimationType.Jump, "Jump");
        m_animationHandler.AddAnimation(AnimationType.Fall, "Jumping Down");
        m_animationHandler.AddAnimation(AnimationType.Push, "Push Stop");
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        m_inputHandler.HandleInputs();
        m_movementHandler.HandleMovement();
        m_animationHandler.HandleAnimation(m_movementHandler.GetVelocity(), m_movementHandler.IsOnTheGround(), m_collisionHandler.CollideAPushable(m_movementHandler.GetDirection()).Key, m_collisionHandler.IsPushing());
        m_collisionHandler.HandleCollision();
    }

    private void Controls(InputDetails l_details)
    {
        if (l_details.m_bindName == "MoveRight") { m_movementHandler.Move(Direction.Right, m_collisionHandler.IsPushing(), m_collisionHandler.CollideAPushable(m_movementHandler.GetDirection())); }
        if (l_details.m_bindName == "MoveLeft") { m_movementHandler.Move(Direction.Left, m_collisionHandler.IsPushing(), m_collisionHandler.CollideAPushable(m_movementHandler.GetDirection())); }
        if (l_details.m_bindName == "Jump") { m_movementHandler.Jump(); }
    }

    private void Pull(InputDetails l_details)
    {
        if (l_details.m_bindName == "Push")
        {
            if(m_collisionHandler.IsPushing()) { return; }
            KeyValuePair<bool, Collider> collision = m_collisionHandler.CollideAPushable(m_movementHandler.GetDirection());
            if (collision.Key) { m_collisionHandler.SetIsPushing(true); }
        }
        if(l_details.m_bindName == "Let")
        {
            if(m_collisionHandler.IsPushing()) { m_collisionHandler.SetIsPushing(false); }
        }
           
    }


    public float m_speed;
    public float m_jumpSpeed;

    private InputHandler m_inputHandler;
    private MovementHandler m_movementHandler;
    private AnimationHandler m_animationHandler;
    private CollisionHandler m_collisionHandler;
}