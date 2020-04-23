using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(gameObject.GetComponent<Item_Pickup>().item.rotation, transform.rotation.y, transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
    
        transform.Rotate(Vector3.up, 50 * Time.deltaTime);
    }
}
