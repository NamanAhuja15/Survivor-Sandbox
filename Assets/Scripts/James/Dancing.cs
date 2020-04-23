using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancing : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    private bool dance;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            dance = true;
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            dance = false;
        }
        anim.SetBool("Dance", dance);
    }
}
