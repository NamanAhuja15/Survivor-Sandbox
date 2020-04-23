using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject selected,target,copy,lefthand;
    public GameObject item_;
    public Item preview;
    public Text object_name,description;
    public GameObject button;
    public bool show;
    public GameObject  slot_prefab, viewport;
    public static InventoryManager instance;
     void Start()
    {
        instance = this;
        button.SetActive(false);
        show = true;
    }
    void Update()
    {
        if (selected != null)
        {
            object_name.text = selected.GetComponent<InventorySlot>().item.Item_Name;
            description.text = selected.GetComponent<InventorySlot>().item.Description;
            button.SetActive(true);
            button.transform.Find("Use").Find("Text").GetComponent<Text>().text = selected.GetComponent<InventorySlot>().item.function;
        }
        else
        {
            object_name.text = null;
            description.text = "";
            button.SetActive(false);
            button.transform.Find("Use").Find("Text").GetComponent<Text>().text = "";
        }
   
    }
    public void Add_slot()
    {
            Instantiate(slot_prefab, viewport.transform);
    }
    public void UpdateData()
    {

        ClearSlots();

        for (int i = 0; i < Inventory.instance.bag.Count; ++i)
        {
            var slot = viewport.transform.GetChild(i).GetComponent<InventorySlot>();
            slot.item = Inventory.instance.bag[i];
            if(slot.item==false)
            {
                Destroy(slot);
                InventoryManager.instance.UpdateData();
            }
            slot.UpdateInfo();
        }


    }

    public void ClearSlots()
    {
        foreach (Transform child in viewport.transform)
        {
            InventorySlot slot = child.GetComponent<InventorySlot>();
            slot.item = null;
            slot.UpdateInfo();
        }
    }

    public void Show()
    {
        if (show)
        {
            if (selected != null )
            {
                preview
                     = selected.GetComponent<InventorySlot>().item;


                if (item_)
                    item_ = null;
                item_ = Instantiate(preview.item, target.transform.position + new Vector3(0, 0, -10), Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z)) as GameObject;
                DestroyImmediate(item_.GetComponent<Rigidbody>(), true);
                item_.layer = 5;
                item_.AddComponent<Rotate>();


            }
            show = false;
        }
        else if (!show)
        {
            show = true;
            Destroy(item_);
        }

       
    }
    public void Use()
    {
        if(selected!=null)
        {
            preview = selected.GetComponent<InventorySlot>().item;

            Destroy(item_);
            selected = null;
            preview.Use();
        }
    }

    public void Drop()
    {
        {
            if(selected!=null)
            {
               copy= Instantiate(selected.GetComponent<InventorySlot>().item.item, lefthand.transform.position, Quaternion.identity) ;
               // copy.AddComponent<BoxCollider>();
                Inventory.instance.Remove(selected.GetComponent<InventorySlot>().item);
               // selected.GetComponent<InventorySlot>().item = null;
               // selected = null;
                Destroy(item_);
            }
        }
    }

}
