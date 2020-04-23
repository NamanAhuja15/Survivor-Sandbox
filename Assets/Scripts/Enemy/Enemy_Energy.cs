using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy_Energy : MonoBehaviour
{
    public float energy, delay;
    private float time;
    public GameObject energy_bar;
    float y, z;
    void Start()
    {
        y = energy_bar.transform.localScale.y;
        z = energy_bar.transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > delay)
        {
            if (energy < 81)
                energy += 20f;
            time = 0f;
        }
        energy_bar.transform.localScale = new Vector3(energy / 100, y, z);
        
    }
}
