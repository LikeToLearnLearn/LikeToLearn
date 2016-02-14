using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExitRaceTrigger : MonoBehaviour {

    public GameObject car;
    public GameObject player;

    private RacingLogic racingLogic;

	// Use this for initialization
	void Start () {

        GameObject racingLogicObject = GameObject.FindWithTag("RacingController");
        if (racingLogicObject != null)
        {
            racingLogic = racingLogicObject.GetComponent<RacingLogic>();
        }
        if (racingLogic == null)
        {
            Debug.Log("Cannot find 'RacingLogic' script");
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void OnTriggerEnter(Collider c)
    {
       // car = GameObject.Find("Car");
        //Debug.Log("collision det with" + c.name);
        if (c.tag.Equals("PlayerCar"))
        //if (c.Equals(GameObject.Find("Car")))
        {
            car.SetActive(false);
            player.SetActive(true);
            GettingMoney(racingLogic.GetPoints());
            racingLogic.SetGameStarted(false);
            car.transform.position = new Vector3(318, 120, 318);
                       
            //Debug.Log("collision det with player");
            //SceneManager.LoadScene("game_racingisland");
        }

    }

    void GettingMoney(float score)
    {
        float Hundredbills = 0;
        float points = score;
        for (int i = 0; i < points/100; i++)
        {
            GameController.control.AddItem(GameController.Item.HundredBill);
            Hundredbills++;
        }
        points = points - (Hundredbills * 100);

        float TenCoins = 0;
        for (int i = 0; i < points / 10; i++)
        {
            GameController.control.AddItem(GameController.Item.TenCoin);
            TenCoins ++;
        }

        points = points - (TenCoins  * 10);
        for (int i = 0; i < points; i++)
        {
            GameController.control.AddItem(GameController.Item.OneCoin);
        }
    }
}
