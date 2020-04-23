using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class Aim : MonoBehaviour
{


    public Image crosshair;
    public Camera cam;
    public Transform muzzle;
    void Start()
    {
       
    }
    void Update()
    {
        cam = Camera.main;
        // if (Input.GetKey(KeyCode.LeftControl))

        /*  RaycastHit hit;
          if (Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit))
          {
             // if (hit.collider)
              {*/
        Vector3 mouspos = Input.mousePosition;
            crosshair.rectTransform.localPosition = cam.ScreenToWorldPoint(mouspos); ;
                
           
       // }
    }
}