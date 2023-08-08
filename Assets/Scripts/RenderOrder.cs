using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOrder : MonoBehaviour
{

    void LateUpdate()
    {
        foreach(var rend in GetComponentsInChildren<Renderer>())
        {
            rend.sortingOrder = -(int) (GetComponent<Collider2D>().bounds.min.y * 1000);
        }
    }

}
