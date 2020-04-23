using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    public float current_health, hunger;
    public float maxhealth, maxhunger;
    private float time;
    public Text life, hungry;
    public GameObject inventory;
    public static Health instance;
    private Animator anim;
    public bool dead;
    public Image health_bar, hunger_bar;
    void Start()
    {
        instance = this;
        current_health = 100f;
        hunger = 0f;
        anim = GetComponent<Animator>();
        dead = false;
        inventory.SetActive(false);
    }
    public void Heal(float amount)
    {
        current_health += amount;
        if (current_health > maxhealth)
        {
            current_health = maxhealth;
        }
    }

    public void Eat(float amount)
    {
        hunger = hunger - amount;
        if(hunger<0)
        {
            hunger = 0;
        }
    }
    private void Update()
    {
        time += Time.deltaTime;
        life.text =current_health.ToString()+"/100";
        hungry.text =hunger.ToString()+"/100";
        if(Input.GetKey(KeyCode.Tab))
        {
            inventory.SetActive(true);
        }
        else if(Input.GetKeyUp(KeyCode.Tab))
        {
            inventory.SetActive(false);
        }
        Hungry();
        if(current_health<=0)
        {
            if (!dead)
            {
                anim.SetTrigger("Die");
                dead = true;
                SceneManager.LoadScene("GameOver");
            } 
        }
        health_bar.fillAmount = current_health / 100;
        hunger_bar.fillAmount = hunger / 100;
        
    }
    public void Hungry()
    {
        if(time>5f)
        {
            if (hunger < maxhunger)
            {
                hunger += 5f;
                time = 0f;
            }
            else if(hunger>maxhunger)
            {
                hunger = maxhunger;
            }
        }
    }
}
