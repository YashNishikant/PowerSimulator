using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Material[] material;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }

    void Update()
    {
        if (FindObjectOfType<SwordBehavior>().tpActive)
        {
            rend.sharedMaterial = material[0];
        }
        else {
            rend.sharedMaterial = material[1];
        }
    }
}
