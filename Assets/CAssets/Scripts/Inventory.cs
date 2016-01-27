using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public enum Items { Fish, Brick, OneCoin, TenCoin, HundredBill, ThousandBill };
    private Dictionary<Items, int> AcquiredItems;

    // Use this for initialization
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
