using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMinkowski : MonoBehaviour
{
    public BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        if(collider == null)
            collider = GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            Debug.LogError("No Collider found in Object : " + gameObject.name);
            return;
        }

        Preparation();
        Generation();
    }

    private void Generation()
    {
        throw new NotImplementedException();
    }

    private void Preparation()
    {
        Bounds verticies = collider.bounds;
        var center = gameObject.transform.position;
        var upperRight = center + new Vector3(1 * transform.localScale.x, 0, 1 * transform.localScale.y);
    }
}
