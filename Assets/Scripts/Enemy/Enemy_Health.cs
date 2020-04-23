using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    public float health, punch_damage,kick_damage;
    private float time;
    private Animator anim;
    public bool dead;
    public GameObject healthbar;
    private float y, z;
    private AudioSource source;
    public AudioClip hit,attack;
    public GameObject hand;
    public ParticleSystem kick,punch;
    void Start()
    {
        health = 100f;
        anim = GetComponent<Animator>();
        dead = false;
        y = healthbar.transform.localScale.y;
        z = healthbar.transform.localScale.z;
        source = this.gameObject.GetComponent<AudioSource>();
        hand.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Hand"))
        {
            if (time > 1f)
            {

                health -= punch_damage;
                time = 0f;
                anim.SetTrigger("Hit");
                source.PlayOneShot(hit);
                Debug.Log(health);
                Instantiate(punch, collision.transform.position, Quaternion.identity);
            }
        }
        if (collision.gameObject.CompareTag("Leg"))
            {

            if (time > 1f)
            {

                health -= kick_damage;
                time = 0f;
                anim.SetTrigger("Hit");
                source.PlayOneShot(hit);
                Debug.Log(health);
                Instantiate(kick, collision.transform.position, Quaternion.identity);
            }
        } 

    }
    public void Attack()
    {
        source.PlayOneShot(attack);
        if (!hand.gameObject.GetComponent<BoxCollider>().enabled)
            hand.gameObject.GetComponent<BoxCollider>().enabled = true;

    }
    public void Idle()
    {
        if (hand.gameObject.GetComponent<BoxCollider>().enabled)
           hand.gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (health < 0f)
        {
            health = 0f;
        }
        if(health==0)
        {
            anim.SetTrigger("Dead");
            dead = true;
            Destroy(this.gameObject, 2f);
        }
        healthbar.transform.localScale = new Vector3(health / 100, y, z);
    }

}
