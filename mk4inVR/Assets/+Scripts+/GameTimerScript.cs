using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerScript : MonoBehaviour
{
    [SerializeField] ManagerScript manager;
    [SerializeField] FinishAreaScript finish;
    [SerializeField] TimeImageScript time;
    [SerializeField] MainUIManagerScript ui;
    //public Score score;
    public float GameTime = 0;
    bool TimerFlag = true;
    string timeText;
    // Start is called before the first frame update
    void Start()
    {
        //timeText = "現在タイム";
    }

    // Update is called once per frame
    void Update()
    {
        if(/*manager.getFol_A_Flag == true && manager.getFol_B_Flag == true && manager.getFol_C_Flag == true && */finish.OnTriggerFin)
        {
            TimerFlag = false;
        }

        if(ui.OnPause) TimerFlag = false;
        else TimerFlag = true;

        if (TimerFlag)
       {
            GameTime += Time.deltaTime;
            time.RandomScore();
        }

        if (finish.OnTriggerFin) timeText = "完了タイム";


        //GetComponent<Text>().text = GameTime.ToString(timeText + "0"+"秒");
    }
}
