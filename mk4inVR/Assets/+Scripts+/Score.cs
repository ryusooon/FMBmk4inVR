using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;



public class Score : MonoBehaviour
{
    public Sprite[] numimage;
    public List<int> number = new List<int>();
    public GameTimerScript time;
    void Start()
    {
        number[0] = 0;
    }
    public void RandomScore()
    {
        //クリックされるたびにrandomでスコアを変動
        //var random = Random.Range(1, 90000);
        var random = (int)time.GameTime;
        //いままで表示されてたスコアオブジェクト削除
        var objs = GameObject.FindGameObjectsWithTag("Score");
        foreach (var obj in objs)
        {
            if (0 <= obj.name.LastIndexOf("Clone"))
            {
                Destroy(obj);
            }
        }
        View(random);
    }
    //スコアを表示するメソッド
    void View(int score)
    {
        var digit = score;
        //要素数0には１桁目の値が格納
        number = new List<int>();
        while (digit != 0)
        {
            score = digit % 10;
            digit = digit / 10;
            number.Add(score);
        }

        GameObject.Find("ScoreImage").GetComponent<Image>().sprite = numimage[number[0]];
        for (int i = 1; i < number.Count; i++)
        {
            //複製
            RectTransform scoreimage = (RectTransform)Instantiate(GameObject.Find("ScoreImage")).transform;
            scoreimage.SetParent(this.transform, false);
            scoreimage.localPosition = new Vector2(
                scoreimage.localPosition.x - scoreimage.sizeDelta.x * i,
                scoreimage.localPosition.y);
            scoreimage.GetComponent<Image>().sprite = numimage[number[i]];
        }
    }
}
