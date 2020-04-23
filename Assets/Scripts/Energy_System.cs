using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Energy_System : MonoBehaviour
{
    public float energy,delay;
    public float time;
    public Image energy_bar;
    public Text energy_count;
   // public Image energy_bar;
    void Start()
    {
        energy = 0;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>delay)
        {
            if(energy<81)
            energy += 20f;
            time = 0f;
        }
        energy_bar.fillAmount = energy / 100;
        energy_count.text = energy.ToString() + "/100";
        //energy_bar.fillAmount = energy / 100;
    }
}
