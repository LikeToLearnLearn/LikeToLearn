using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public enum Item { Fish, Brick, OneCoin, TenCoin, HundredBill, ThousandBill };
    private Dictionary<Item, int> acquiredItems;
    public static Dictionary<Item, int> itemValue = new Dictionary<Item, int>() {
        { Item.OneCoin, 1 },
        { Item.TenCoin, 10 },
        { Item.HundredBill, 100 },
        { Item.ThousandBill, 1000 },
        { Item.Brick, 50 },
        { Item.Fish, 12 }
    };
    
    public GameObject inventoryCanvas;
    public GameObject dynamicPanel; 

    private bool inventoryWindowToggle = false;
    private Rect inventoryWindowRect = new Rect(300, 100, 400, 400);

    private bool redraw = true;


    // Use this for initialization
    void Start () {
        AddItem(Item.OneCoin);
        AddItems(Item.Fish, 30);
	}
	
	// Update is called once per frame
	void Update () {
        if (inventoryCanvas.activeSelf && redraw)
        {
            DrawItems();
            redraw = false;
        }
	}

    private void DrawItems()
    {
        if (acquiredItems == null)
            return;
        foreach (var child in dynamicPanel.GetComponents<Text>())
            Destroy(child);
        foreach (var kvp in acquiredItems)
        {
            //var go = gameObject.GetComponent<Text>();
            //go.text = kvp.Key + " x " + kvp.Value;
            //go.transform.SetParent(dynamicPanel.transform);
            print(kvp.Key + " x " + kvp.Value + " (Value: " + (kvp.Value * itemValue[kvp.Key]) + ")");
        }
    }

    public void AddItems(Item i, int count)
    {
        if (acquiredItems == null)
           acquiredItems = new Dictionary<Item, int>();
        int old = acquiredItems.ContainsKey(i) ? acquiredItems[i] : 0;
        acquiredItems[i] = old + count;
        redraw = true;
    }

    public void AddItem(Item i)
    {
        AddItems(i, 1);
    }


    void OnGui()
    {
        inventoryWindowToggle = GUI.Toggle(new Rect(800, 50, 100, 50), inventoryWindowToggle, "Inventory");

        if (inventoryWindowToggle)
        {
            inventoryWindowRect = GUI.Window(0, inventoryWindowRect, InventoryWindowMethod, "Inventory");

        }

       

    }

 void InventoryWindowMethod(int windowId)
        {
        GUILayout.BeginArea(new Rect(0, 50, 400, 400));

        GUILayout.BeginHorizontal();
        GUILayout.Button("Item 1", GUILayout.Height(50));
        GUILayout.Button("Item 2", GUILayout.Height(50));
        GUILayout.Button("Item 3", GUILayout.Height(50));
        GUILayout.EndHorizontal();


    }
}
