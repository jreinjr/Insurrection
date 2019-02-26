using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct Hotkey
{
    public KeyCode key;
    public UnityEvent action;
    public TargetableEvent test;
}

[System.Serializable]
public class TargetableEvent : UnityEvent<GameObject> { }

public class InputHandler : MonoBehaviour
{
    public Hotkey[] keys;

    void Update()
    {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (Input.GetKeyDown(keys[i].key))
                {
                    keys[i].action.Invoke();
                }
            }
        }
    }

    
}
