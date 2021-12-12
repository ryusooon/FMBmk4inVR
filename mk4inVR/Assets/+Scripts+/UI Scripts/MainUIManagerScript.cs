using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManagerScript : MonoBehaviour
{

    [SerializeField] CanvasGroup Pausemanu;
    [SerializeField] CanvasGroup Soundmanu;
    [SerializeField] CanvasGroup Contllolmanu;
    [SerializeField] CanvasGroup MainCanvas;
    [SerializeField] CanvasGroup ResultCanvas;
    [SerializeField] FinishAreaScript FinScript;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//取りあえずいったんEscapeKeyでManuを開くようにする
        {
            Debug.Log("Pause押したよ");
            CanvasGroupOnOf(Pausemanu,true);
            CanvasGroupOnOf(MainCanvas, false);
        }

        if(FinScript.OnTriggerFin)
        {
            CanvasGroupOnOf(MainCanvas, false);
            CanvasGroupOnOf(ResultCanvas, true);
        }
    }

    public void ExitPauseManu()//Pause画面→元の状態へ　　　　あとでゲーム自体の停止も加える？
    {

        CanvasGroupOnOf(Pausemanu, false);
        CanvasGroupOnOf(MainCanvas, true);

    }

    public void ExitResultCanvas()
    {
        CanvasGroupOnOf(MainCanvas, true);
        CanvasGroupOnOf(ResultCanvas, false);
    }

    public void BackTitle()//タイトル画面に戻る
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void pushSoundButton()
    {
        CanvasGroupOnOf(Soundmanu, true);
        CanvasGroupOnOf(Pausemanu, false);
    }
    public void pushContllolButton()
    {
        CanvasGroupOnOf(Contllolmanu, true);
        CanvasGroupOnOf(Pausemanu, false);
    }
    public void BackPauseManu1()
    {
        CanvasGroupOnOf(Soundmanu, false);
        CanvasGroupOnOf(Pausemanu, true);
    }

    public void BackPauseManu2()
    {
        CanvasGroupOnOf(Contllolmanu, false);
        CanvasGroupOnOf(Pausemanu, true);

    }

    private void CanvasGroupOnOf(CanvasGroup canvasGroup, bool enable)
    {
        if (enable)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

    }
}
