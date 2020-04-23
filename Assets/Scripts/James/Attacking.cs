using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Attacking : MonoBehaviour
{
    private Animator animator;public GameObject lefthand, righthand;
    int noOfClicks = 0;
    bool attack;
    //Time when last button was clicked
    float lastClickedTime = 0;
    float timez;
    //Delay between clicks for which clicks will be considered as combo
   public float maxComboDelay = 0.3f;
    private AudioSource source;
    public AudioClip punch;
    public Text display;
    public GameObject leg;
     void Start()
    {
        animator = GetComponent<Animator>();
        timez = 0;
        source = GetComponent<AudioSource>();
        display.enabled = false;
    }
    void Update()
    {
        if (Health.instance.dead == false)
        {
            if (attack)
            {
                lefthand.GetComponent<BoxCollider>().enabled = true;
                righthand.GetComponent<BoxCollider>().enabled = true;
            }
            else
            {
                lefthand.GetComponent<BoxCollider>().enabled = false;
                righthand.GetComponent<BoxCollider>().enabled = false;
            }
            if (Time.time - lastClickedTime > maxComboDelay)
            {
                noOfClicks = 0;
            }
            timez += Time.deltaTime;
            if (noOfClicks == 0)
            {
                attack = false;
                animator.SetBool("Attack", attack);
                animator.SetFloat("Punch", 0f);
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (timez > 0.5f)
                {
                    attack = true;
                    animator.SetBool("Attack", attack);
                    OnClick();
                    timez = 0f;
                }
            }
            if(Input.GetMouseButtonDown(1))
            {
                if (this.gameObject.GetComponent<Energy_System>().energy > 30f)
                {
                    this.gameObject.GetComponent<Energy_System>().energy -= 30f;
                    this.gameObject.GetComponent<Energy_System>().time = 0f;
                    {
                        animator.SetTrigger("Kick");
                        this.gameObject.GetComponent<MovementInput>().blockRotationPlayer = true;
                    }
                }
                else
                {
                    if (display.enabled == false)
                    {
                        display.text = "Insufficient Energy";
                        display.enabled = true;
                        timez = 0f;
                    }
                }
            }
            if (timez>2f)
                display.enabled = false;
        }
        
    }
    //Call on button click
    void OnClick()
    {
        //Record time of last button click
        lastClickedTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 2)
        {
            animator.SetFloat("Punch", 0.7f);
        }
        source.PlayOneShot(punch);
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 2);
    }
    public void Reset()
    {
        this.gameObject.GetComponent<MovementInput>().blockRotationPlayer = false;
        this.gameObject.GetComponent<Energy_System>().time = 0f;
        leg.GetComponent<BoxCollider>().enabled = false;
    }
    public void Kick()
    {
        leg.GetComponent<BoxCollider>().enabled = true;
    }
}
