using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Item : ScriptableObject
{
    public string Item_Name,Description;
    public float weight;
    public enum Type { Consumables,Ammunition,Weapons};
    public Type type;
    public GameObject item;
    public Sprite icon;
    public string function;
    public float rotation;
    public virtual void Use()
    { 
    }
    private void Awake()
    {
       
    }
}
