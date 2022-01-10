﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeImageScript : MonoBehaviour
{
    public Sprite[] numimage;
    public List<int> number = new List<int>();
    public GameTimerScript time;
    public void RandomScore()
    {
        
        var random = (int)time.GameTime;

        var objs = GameObject.FindGameObjectsWithTag("TimeUI");
        foreach (var obj in objs)
        {
            if (0 <= obj.name.LastIndexOf("Clone"))
            {
                Destroy(obj);
            }
        }
        View(random);
    }
    

    void View(int score)
    {
        var digit = score;
        
        number = new List<int>();
        while (digit != 0)
        {
            score = digit % 10;
            digit = digit / 10;
            number.Add(score);
        }

        GameObject.Find("TimeImage").GetComponent<Image>().sprite = numimage[number[0]];
        for (int i = 1; i < number.Count; i++)
        {
           
            RectTransform scoreimage = (RectTransform)Instantiate(GameObject.Find("TimeImage")).transform;
            scoreimage.SetParent(this.transform, false);
            scoreimage.localPosition = new Vector2(
                scoreimage.localPosition.x - scoreimage.sizeDelta.x * i,
                scoreimage.localPosition.y);
            scoreimage.GetComponent<Image>().sprite = numimage[number[i]];
        }
    }
}
