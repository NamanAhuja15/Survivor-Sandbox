using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Parasite : MonoBehaviour
{
    private Animator animator;
    public ParticleSystem spit;
    public GameObject acid;
    float time;
    private NavMeshAgent nav;
    void Start()
    {
        animator = GetComponent<Animator>();
        acid.SetActive(false);
        nav = GetComponent<NavMeshAgent>();
        acid.GetComponent<BoxCollider>().enabled = false;
    }
    public void Stop()
    {
        spit.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (this.gameObject.GetComponent<Enemy_Energy>().energy >= 100)
        {
            animator.SetTrigger("Spit");
            this.gameObject.GetComponent<Enemy_Energy>().energy = 0f;
            spit.Play();
            acid.SetActive(true);
            time = 0f;
            nav.isStopped = true;
            acid.GetComponent<BoxCollider>().enabled = true;
        }
        if (acid.activeSelf)
        {
            if (time > 5f)
            {
                acid.SetActive(false);
                nav.isStopped = false;
                acid.GetComponent<BoxCollider>().enabled = false;
            }
        }

    }
}
