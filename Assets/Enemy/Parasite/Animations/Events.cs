using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    public ParticleSystem attack;
    public GameObject hand;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Effect()
    {
        Instantiate(attack,hand.transform.position, Quaternion.identity);
    }
}
