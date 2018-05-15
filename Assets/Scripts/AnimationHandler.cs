using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationType { Idle, Run, Jump, Fall, Push }

public class AnimationHandler {

    public AnimationHandler(string l_name)
    {
        m_player = GameObject.Find(l_name);
        m_animator = m_player.GetComponentInChildren<Animator>();
        m_current = AnimationType.Idle;
        m_convertType = new Dictionary<AnimationType, string>();
    }

    public void HandleAnimation(Vector3 l_velocity, bool l_isOnTheGround, bool l_canPull, bool l_isPushing)
    {
        if (Mathf.Abs(l_velocity.z) > 0.1f && m_current != AnimationType.Run && l_isOnTheGround) { SetAnimation(AnimationType.Run); }
        if (l_velocity.z == 0.0f && m_current == AnimationType.Run) { SetAnimation(AnimationType.Idle);  }
        if (l_velocity.y > 0.1f && m_current != AnimationType.Jump) { SetAnimation(AnimationType.Jump); }
        if (l_isOnTheGround && m_current == AnimationType.Jump) { SetAnimation(AnimationType.Idle); }
        if (l_canPull && l_isPushing && m_current != AnimationType.Push) { SetAnimation(AnimationType.Push); }
        if (!l_isPushing && m_current == AnimationType.Push) { SetAnimation(AnimationType.Idle); }

        //if (l_velocity.y < 0 && m_current == AnimationType.Jump) { SetAnimation(AnimationType.Fall); }
    }

    public void AddAnimation(AnimationType l_type, string l_name)
    {
        if(m_convertType.ContainsKey(l_type)) { return; }

        m_convertType.Add(l_type, l_name);
    }

    public void SetAnimation(AnimationType l_type)
    {
        if (m_current == l_type) { return; }

        m_current = l_type;
        m_animator.Play(m_convertType[l_type]);
    }

    private GameObject m_player;
    private Animator m_animator;
    private AnimationType m_current;
    private Dictionary<AnimationType, string> m_convertType;
}