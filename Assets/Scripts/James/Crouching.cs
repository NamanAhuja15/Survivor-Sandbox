using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouching : MonoBehaviour
{
    CharacterController ch;public CharacterController controller;
    float startheight, lastheight, newheight;
    public float increase,height_increase;
    public  bool crouching;
    Animator animator;
    private bool crouchwalking,rolling;
    public GameObject player;
    
    void Start()
    {
        ch = GetComponent<CharacterController>();
//t = GetComponent<Transform>();
        startheight = ch.height;
        animator = GetComponent<Animator>();
         
    }

    // Update is called once per frame
    void Update()
    {
        Crouch();
        
        
        if(crouching&&player.GetComponent<MovementInput>().dummy>0.2f&& player.GetComponent<MovementInput>().dummy <0.8f)
        {
            crouchwalking = true;
        }
        else
        {
            crouchwalking = false;
        }
        if (player.GetComponent<MovementInput>().dummy > 0.8f&&player.GetComponent<MovementInput>().Sprinting)
        {
            if (!rolling)
            {
                if (Input.GetKeyDown(KeyCode.C))
                rolling = true;
            }
        }
        else if(Input.GetKeyUp(KeyCode.C))
            rolling = false;
        animator.SetBool("Crouching", crouching);
        animator.SetBool("CrouchWalk", crouchwalking);
        animator.SetBool("Roll", rolling);
        //controller.Move(new Vector3(0, height_increase, 0));



    }

    public void Crouch()
    {
        newheight = startheight;
        if (Input.GetKeyDown(KeyCode.C) && gameObject.GetComponent<MovementInput>().Sprinting == false)
        {
            controller.center -= new Vector3(0, -height_increase, 0);
        }
        if (Input.GetKey(KeyCode.C) && !gameObject.GetComponent<MovementInput>().Sprinting == false)
        {
            newheight = increase * startheight;

            crouching = true;
            // if(MovementInput.)
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            crouching = false;
           // rolling = false;
            if (!rolling)
                controller.center += new Vector3(0, height_increase, 0);
        }
        lastheight = ch.height;
        ch.height = Mathf.Lerp(ch.height, newheight, 5f * Time.deltaTime);

    }
}
