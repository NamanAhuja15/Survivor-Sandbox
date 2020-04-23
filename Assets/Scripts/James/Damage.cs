using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Damage : MonoBehaviour
{
    private float time;
    private AudioSource source;
    public AudioClip hurt;
    public float parasite, clown;
    public ParticleSystem punch;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        source = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider hit)
    {
        if(hit.gameObject.CompareTag("p_punch"))
        {
            if (time > 0.5f)
            {
                time = 0f;
                Health.instance.current_health -= parasite;
                source.PlayOneShot(hurt);
                Instantiate(punch, hit.transform.position, Quaternion.identity);
            }
        }
        if (hit.gameObject.CompareTag("c_punch"))
        {
            if (time > 0.5f)
            {
                time = 0f;
                Health.instance.current_health -= clown;
                source.PlayOneShot(hurt);
                Instantiate(punch, hit.transform.position, Quaternion.identity);
            }
        }
        if (hit.gameObject.CompareTag("acid"))
        {
            if(time>0.5f)
            {
                time = 0f;
                Health.instance.current_health -= parasite;
                source.PlayOneShot(hurt);

            }
            
        }
    }
    void Update()
    {
        time += Time.deltaTime;
    }
}
