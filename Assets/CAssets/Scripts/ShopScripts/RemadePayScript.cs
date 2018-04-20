using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RemadePayScript : MonoBehaviour {
    public int balance = GameController.control.GetBalance();
    string selectedItem = "";
    [System.Serializable]
    public class UIText
    {
        public Text itemName;
        public Text itemPrice;
        public Text itemAmount;
        public Button buttonConfirm;
        public Button buttonCancel;
        public Text balance;
    }
    public UIText shopElements;
    
    // Use this for initialization
    void Start () {
        shopElements.balance.text = "Balance: $" + balance;
    }

    // Update is called once per frame
    void Update () {
     
    }

    public void itemButtonPressed(Item item)
    {
        selectedItem = item.ToString();
    }
}
