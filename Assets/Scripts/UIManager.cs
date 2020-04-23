using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text message;
    public static string display;
    public static bool show;
    public static float health;
    public float time;
    void Start()
    {
        message.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (show)
        {
            message.text = display;
            message.enabled = true;
            Invoke("Disable", 2f);
        }
        if (!show)
        {
            message.enabled = false;
        }

    }
    void Disable()
    {
        show = false;
    }
}
