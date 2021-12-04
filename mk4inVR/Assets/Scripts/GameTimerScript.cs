using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerScript : MonoBehaviour
{
    [SerializeField] ManagerScript manager;
    float GameTime = 0;
    bool TimerFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.getFol_A_Flag == true && manager.getFol_B_Flag == true && manager.getFol_C_Flag == true)
        {
            TimerFlag = false;
        }

        if (TimerFlag)
        {
            GameTime += Time.deltaTime;
        }
        GetComponent<Text>().text = GameTime.ToString("現在タイム" + "0");
    }
}
