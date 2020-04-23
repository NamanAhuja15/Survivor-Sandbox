using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu (fileName = "new Consumable.asset",menuName = "Items/Consumables")]
public class Consumables : Item
{
    public int heal = 0;
    public int hunger = 0;

    public override void Use()
    {
        GameObject player = Inventory.instance.player;
        Health playerhealth = player.GetComponent<Health>();
        playerhealth.Heal(heal);
        playerhealth.Eat(hunger);   
        Inventory.instance.Remove(this);
    }
}
