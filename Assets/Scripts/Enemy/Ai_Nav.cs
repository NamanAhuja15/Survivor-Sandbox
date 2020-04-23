using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai_Nav : MonoBehaviour
{
    private NavMeshAgent nav;
    public GameObject destination;
    private Animator anim;
    private float speed,gravity,time;
    private bool stand, attack,dead;
    public GameObject icon;
    //public ParticleSystem p_punch, c_punch;

    //private CharacterController enemy;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
     
        gravity = 1f;
        icon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        dead = gameObject.GetComponent<Enemy_Health>().dead;
        if (!dead)
        {
            if (Vector3.Distance(transform.position, destination.transform.position) <= 20f)
            {
                stand = true;
                icon.SetActive(true);
            }
            else
            {
                stand = false;
                nav.isStopped = true;
                icon.SetActive(false);
            }
            if (Vector3.Distance(transform.position, destination.transform.position) <= 1.5f)
            {
                attack = true;
                nav.transform.LookAt(destination.transform);
                nav.isStopped = true;
              
            }
            else if (Vector3.Distance(transform.position, destination.transform.position) > 2f && stand)
            {
                attack = false;
                nav.isStopped = false;
                nav.SetDestination(destination.transform.position);
            }
    
            speed = nav.velocity.magnitude / 2;
        

            anim.SetBool("Stand", stand);
            anim.SetBool("Attack", attack);
            anim.SetFloat("Blend", speed);
        }
    }
   /* public void Effect()
    {
        if (this.gameObject.name == "Parasite")
        {
            Instantiate(p_punch, hand.transform.position, Quaternion.identity);
        }
        else if (this.gameObject.name == "Clown")
        {
            Instantiate(c_punch, hand.transform.position, Quaternion.identity);
        }
    }*/
}
