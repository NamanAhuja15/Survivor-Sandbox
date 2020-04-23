using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Pickup : MonoBehaviour
{
    private GameObject target;
   // public Text pose;
    private float time; private bool pickup, equip;
    public float distance;
    public Item item;
    public Text pose;
    void Start()
    {
        time = 0f;
       // pose.enabled = false;
        target = GameObject.FindGameObjectWithTag("Player");
        pickup = false;
        equip = false;
        pose.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (pose.enabled)
        {
            if (time > 2f)
                pose.enabled = false;
        }
        if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= distance && !pickup && !equip)
        {
            DisplayMessage("Press E to pick up");
        }
        if (pickup)
        {
            if (time > 0.5f)
            {
                equip = true;
            }

        }
        if (equip)
        {
            Destroy(gameObject.GetComponent<BoxCollider>());
          /*  DisplayMessage("Press E to collect or B to drop");

            if (Input.GetKeyDown(KeyCode.E))
            {
                pose.enabled = false;
                equip = false;
                Debug.Log("Equiiped");
                Inventory.instance.Add(item); 
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                pose.enabled = false;
                equip = false;
                gameObject.transform.SetParent(this.gameObject.transform);
            }

            Pickup();*/
        }
    }
    void DisplayMessage(string str)
    {
        pose.enabled = true;
        pose.text = str;
    }
    public void Pickup()
    {

        /*  if (equip)
          {
              gameObject.transform.position = target.transform.position;
              gameObject.transform.parent = target.transform;
              gameObject.transform.localScale = target.transform.localScale;
          }
          */
        // else
        //  Destroy(gameObject.GetComponent<BoxCollider>());
        if (time > 0.3f)
        {
            time = 0f;
            // pickup = true;
            Inventory.instance.Add(item);

            return;
        }
    }
}
