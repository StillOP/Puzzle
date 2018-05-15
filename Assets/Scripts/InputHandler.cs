using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InputType { Default, KeyDown, KeyUp }

public struct InputDetails
{
    public string m_bindName;
    public KeyCode m_key;

    public InputDetails(string l_bindName, KeyCode l_key)
    {
        m_bindName = l_bindName;
        m_key = l_key;
    }
}

public struct InputInfos
{
    public KeyCode m_keyCode;
    public InputType m_type;

    public InputInfos(KeyCode l_keyCode, InputType l_type)
    {
        m_keyCode = l_keyCode;
        m_type = l_type;
    }
}

public class InputHandler {

    public InputHandler() { m_bindings = new Dictionary<string, KeyValuePair<InputInfos, Action<InputDetails>>>(); }

    public void BindInput(string l_name, KeyCode l_key, InputType l_type, Action<InputDetails> l_bind)
    {
        if(m_bindings.ContainsKey(l_name)) { return;  }

        InputInfos inputInfos = new InputInfos(l_key, l_type);
        m_bindings[l_name] = new KeyValuePair<InputInfos, Action<InputDetails>>(inputInfos, l_bind);
    }

    public void RemoveBinding(string l_name)
    {
        if (!m_bindings.ContainsKey(l_name)) { return; }

        m_bindings.Remove(l_name);
    }

    public void HandleInputs()
    {
        foreach(KeyValuePair<string, KeyValuePair<InputInfos, Action<InputDetails>>> pair in m_bindings)
        {
            if (GetInputFunc(pair.Value.Key.m_type)(pair.Value.Key.m_keyCode))
            {
                InputDetails details = new InputDetails(pair.Key, pair.Value.Key.m_keyCode);
                pair.Value.Value(details);
            }
        }
    }

    public Func<KeyCode, bool> GetInputFunc(InputType l_type)
    {
        if (l_type == InputType.KeyDown) { return Input.GetKeyDown; }
        else if (l_type == InputType.KeyUp) { return Input.GetKeyUp; }

        return Input.GetKey;
    }

    private Dictionary<string, KeyValuePair<InputInfos, Action<InputDetails>>> m_bindings;
}