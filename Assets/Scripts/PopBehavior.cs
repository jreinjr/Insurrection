using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Winding
{
    CLOCKWISE,
    COUNTERCLOCKWISE
}

public class PopBehavior : MonoBehaviour
{
    public float popSpeed;

    private PopCensus popCensus;

    private void Start()
    {
        popCensus = GetComponent<PopCensus>();
    }


    Vector3 MoveAlongPath(Vector3 currentPos, Winding winding, float speed)
    {
        if (winding == Winding.COUNTERCLOCKWISE)
            speed *= -1;

        if (Mathf.Abs(currentPos.x) > Mathf.Abs(currentPos.z))
        {
            return Vector3.back * speed * Mathf.Sign(currentPos.x) * Time.deltaTime;
        }
        else //  | currentPos.x |  <=  | currentPos.z |
        {
            return Vector3.right * speed * Mathf.Sign(currentPos.z) * Time.deltaTime;
        }
    }

    void Update()
    {
        foreach(var go in PopCensus.gameObjectPopMap)
        {
            var t = go.Key.transform;
            var p = go.Value;
            t.Translate(MoveAlongPath(t.position, p.winding, p.speed));
        }
    }
}
