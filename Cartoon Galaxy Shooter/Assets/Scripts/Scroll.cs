using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    float parralax = 15f;

    // Update is called once per frame
    void Update()
    { 
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.y += Time.deltaTime / parralax;

        if (Input.GetKey("right"))
        {
            offset.x += Time.deltaTime / 50f;
        }
        if (Input.GetKey("left"))
        {
            offset.x += -(Time.deltaTime / 50f);
        }

        mat.mainTextureOffset = offset;
    }
}
