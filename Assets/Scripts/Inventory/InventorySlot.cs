using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    public Item item;
    private GameObject target, item_;
    public Text count;
    public void Use()
    {
        if (item)
        {
            item.Use();
            //   UseDrop.usedrop.ta
        }
    }

    public void Select()
    {
        if(InventoryManager.instance.selected!=this.gameObject&&InventoryManager.instance.selected!=null)
        {
            InventoryManager.instance.selected.transform.Find("Borders").gameObject.SetActive(false);
            InventoryManager.instance.selected = null;
            if (InventoryManager.instance.item_)
            {
                Destroy(InventoryManager.instance.item_);
                InventoryManager.instance.show = true;
            }
            
        }
        InventoryManager.instance.selected = this.gameObject;
        gameObject.transform.Find("Borders").gameObject.SetActive(true);

    }
    public void UpdateInfo()
    {
        Text display = transform.Find("Text").GetComponent<Text>();
        Image image_disp = transform.Find("Image").GetComponent<Image>();
        if(item)
        {
            image_disp.enabled = true;
            display.text = item.Item_Name;
            image_disp.sprite = item.icon;
            if (Inventory.mapNameToCount[item.Item_Name] >= 1)
                count.text = Inventory.mapNameToCount[item.Item_Name].ToString();
            else
                count.text = "";
        }
        else
        {
            display.text = "";
            image_disp.enabled = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // UpdateInfo();
        if (InventoryManager.instance.selected != this.gameObject)
            gameObject.transform.Find("Borders").gameObject.SetActive(false);
        if (!item)
            Destroy(gameObject);
    }
}
