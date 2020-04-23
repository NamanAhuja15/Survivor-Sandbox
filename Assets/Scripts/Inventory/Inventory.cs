using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public List<Item> bag = new List<Item>();
    public static int i;
    public int slot_count;
    public static float bag_weight, max_weight;
    public static Inventory instance;
    public GameObject player;
    private bool remove;
    private int index, c;
    public static Dictionary<string, int> mapNameToCount = new Dictionary<string, int>();
    public InventoryManager inven;
    public Text weight;

    void Start()
    {
        instance = this;
        i = 0;
        max_weight = 200f;
        index = -1;
        slot_count = 0;
        c = 0;
        bag_weight = 0f;
      
        mapNameToCount.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        //  Add_slot();
        // Debug.Log(mapNameToCount.Values);
        weight.text = bag_weight.ToString() + "/200";
    }


    public bool Add(Item alpha)
    {
        if (bag_weight+alpha.weight <= max_weight)
        {
           

            if (mapNameToCount.ContainsKey(alpha.Item_Name))
            {
                if(mapNameToCount[alpha.Item_Name] == 0)
                {
                    bag.Add(alpha);
                    inven.Add_slot();
                }
                mapNameToCount[alpha.Item_Name] = mapNameToCount[alpha.Item_Name] + 1;
                Debug.Log(mapNameToCount[alpha.Item_Name]);

            }
          
            else if(mapNameToCount.ContainsKey(alpha.Item_Name)==false)
            {

                bag.Add(alpha);
                inven.Add_slot();
                mapNameToCount.Add(alpha.Item_Name, 1);
                Debug.Log(mapNameToCount[alpha.Item_Name]);

                bag_weight += alpha.weight;


                
            }
            bag_weight += alpha.weight;
            inven.UpdateData();
            return true;
            
        }
        return false;
    }


    public void Remove(Item alpha)
    {
        if (mapNameToCount[alpha.Item_Name] ==1)
        {
            
            
            mapNameToCount[alpha.Item_Name]--;
           // mapNameToCount.Remove(alpha.Item_Name);
            bag.Remove(alpha);
           // InventoryManager.instance.UpdateData();

        }
        else
            mapNameToCount[alpha.Item_Name]--;

        inven.UpdateData();
    }
}
