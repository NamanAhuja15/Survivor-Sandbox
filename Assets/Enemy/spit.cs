using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spit : MonoBehaviour
{
    private Animator anim;
    private float time;
    public ParticleSystem poision;
    public GameObject mouth;
    void Start()
    {
        anim = GetComponent<Animator>();
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(anim.GetBool("Attack")==true)
        {
            if(time>10f)
            {
                anim.SetTrigger("Spit");
                time = 0f;
            }
            
        }
    }
    public void Spit()
    {
        Instantiate(poision, mouth.transform.position, Quaternion.identity);
    }
}
