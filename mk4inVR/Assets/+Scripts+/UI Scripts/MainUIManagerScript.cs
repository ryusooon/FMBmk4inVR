using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;
using System;
using System.Globalization;


public class MainUIManagerScript : MonoBehaviour
{

    [SerializeField] CanvasGroup Pausemanu;
    [SerializeField] CanvasGroup Soundmanu;
    [SerializeField] CanvasGroup Contllolmanu;
    [SerializeField] CanvasGroup MainCanvas;
    [SerializeField] CanvasGroup ResultCanvas;
    [SerializeField] FinishAreaScript FinScript;

    [SerializeField] GameObject PausemanuObj;
    [SerializeField] GameObject SoundmanuObj;
    [SerializeField] GameObject ContllolmanuObj;
    [SerializeField] GameObject MainCanvasObj;
    [SerializeField] GameObject ResultCanvasObj;

    public Camera Camera;
    public GameObject houki;
    [SerializeField] GameObject FakeRay;

    public bool OnPause = false;
    private SteamVR_Action_Boolean Grab = SteamVR_Actions.default_GrabGrip;
    private SteamVR_Action_Boolean Trigger = SteamVR_Actions._default.InteractUI;
    private Boolean GrabGrip;
    private Boolean TriggerOn;

    private bool OnTriggerHR = false;

    public Button button;
    private int nowManu = 0;
    public GameObject FakeObj;

    private int UIX = 200 ;
    private int UIY = 100 ;
    private int UIZ = -6 ;

    private Vector3 UIpos1 = new Vector3(300,75,-30);
    private Vector3 UIpos2 = new Vector3(320,75,-30);

    public PlayerVRScript playerVRScript;
    //public FinishAreaScript finareascript;
    //public Transform UIp1;
    //public Transform UIp2;

    public GameObject Canvass;
    public GameObject up;

    // Start is called before the first frame update
    void Start()
    {
        Canvass= GameObject.Find("CanvasPos");
    }

    // Update is called once per frame
    void Update()
    {
        GrabGrip = Grab.GetState(SteamVR_Input_Sources.RightHand);
        TriggerOn = Trigger.GetState(SteamVR_Input_Sources.RightHand);

        if(GrabGrip||Input.GetKeyDown(KeyCode.Escape)&&nowManu == 0) //横ボタンでTrueになる
        {
            CanvasGroupOnOf(Pausemanu, true);
            CanvasGroupOnOf(MainCanvas, false);
            nowManu = 1;
            OnTriggerHR = true;
            UIX = 180;
            //Pausemanu.rectTransform.anchoredPosition(-20, 0, 0);



            PausemanuObj.GetComponent<RectTransform>().anchoredPosition = UIpos2;

           // Vector3 tmp = GameObject.Find("up").transform.position;
            //tmp =  transform.TransformPoint(tmp);

            //Canvass.transform.parent = up.transform;


            //public Vector3 TransformPoint(Vector3 position);
            //Vector3 t= transform.TransformPoint(UIp1);
            //GameObject.Find("CanavasPos").transform.position = new Vector3(tmp.x , tmp.y, tmp.z);


           // Canvass.transform.position = new Vector3(tmp.x , tmp.y, tmp.z);


            //Canvass.transform.position = up.transform.position;


            //Canvass.transform.parent = null;

        }

        if (Input.GetKeyDown(KeyCode.Escape)||OnTriggerHR /*|| GrabGrip*/)//取りあえずいったんEscapeKeyでManuを開くようにする
        {

            Debug.Log("Pause押したよ");
            //CanvasGroupOnOf(Pausemanu, true);
            //CanvasGroupOnOf(MainCanvas, false);
            OnPause = true;

            //button = PausemanuObj.GetComponent<Button>();
            //button.Select();

            
        }

        if(OnPause)
        {
            //CanvasGroupOnOf(Pausemanu, true);
            //CanvasGroupOnOf(MainCanvas, false);

            if (GrabGrip || Input.GetKeyDown(KeyCode.Escape))//中に入れる状態で再度押したらfalseになるようにしてみる
            {
               // OnTriggerHR = false;
               // OnPause = false;
            }


            RaycastHit hitObj;
            Ray ray = new Ray(houki.transform.position, houki.transform.forward);
            FakeRay.SetActive(true);
            if (Physics.Raycast(ray, out hitObj))
            {
                //Debug.Log(hitObj);
                //Debug.DrawRay(ray.origin, ray.direction * 15f, Color.green, 5, false);

                if(hitObj.collider.gameObject == null)button = FakeObj.GetComponent<Button>();

                button = hitObj.collider.gameObject.GetComponent<Button>();
                button.Select();

                if (hitObj.collider.gameObject.name == "ExitPauseManu" && TriggerOn /*|| GrabGrip*/&&nowManu == 1)
                {
                    nowManu = 0;
                    OnTriggerHR = false;
                    ExitPauseManu();
                    
                }


                if (hitObj.collider.gameObject.name == "BackTitle" && TriggerOn /*|| GrabGrip*/&&nowManu == 1)
                {
                    nowManu = 0;
                    BackTitle();
                }

                if (hitObj.collider.gameObject.name == "GoSoundManu" && TriggerOn /*|| GrabGrip*/&&nowManu == 1)
                {
                    nowManu = 2;
                    PausemanuObj.GetComponent<RectTransform>().anchoredPosition = UIpos1;
                    SoundmanuObj.GetComponent<RectTransform>().anchoredPosition = UIpos2;
                    pushSoundButton();
                }

                if (hitObj.collider.gameObject.name == "GoContllol" && TriggerOn /*|| GrabGrip*/&&nowManu == 1)
                {
                    nowManu = 3;
                    PausemanuObj.GetComponent<RectTransform>().anchoredPosition = UIpos1;
                    ContllolmanuObj.GetComponent<RectTransform>().anchoredPosition = UIpos2;
                    pushContllolButton();
                }

                if (hitObj.collider.gameObject.name == "ExitthisManu" && TriggerOn /*|| GrabGrip*/&&nowManu == 2)
                {
                    nowManu = 1;
                    PausemanuObj.GetComponent<RectTransform>().anchoredPosition = UIpos2;
                    SoundmanuObj.GetComponent<RectTransform>().anchoredPosition = UIpos1;
                    BackPauseManu1();
                }

                if (hitObj.collider.gameObject.name == "ExitCManu" && TriggerOn /*|| GrabGrip*/&&nowManu == 3)
                {
                    nowManu = 1;
                    PausemanuObj.GetComponent<RectTransform>().anchoredPosition = UIpos2;
                    ContllolmanuObj.GetComponent<RectTransform>().anchoredPosition = UIpos1;
                    BackPauseManu2();
                }

                if (hitObj.collider.gameObject.name == "BackTitle1" && TriggerOn /*|| GrabGrip*/)//Finishが関係空いてくる後でやる
                {
                    ResultCanvasObj.GetComponent<RectTransform>().anchoredPosition = UIpos2;
                    PausemanuObj.GetComponent<RectTransform>().anchoredPosition = UIpos1;
                    BackTitle();
                }

                if (hitObj.collider.gameObject.name == "ExitResult" && TriggerOn /*|| GrabGrip*/)
                {
                    ResultCanvasObj.GetComponent<RectTransform>().anchoredPosition = UIpos1;
                    PausemanuObj.GetComponent<RectTransform>().anchoredPosition = UIpos2;
                    ExitResultCanvas();
                }


            }
        }
        else
        {
            //CanvasGroupOnOf(Pausemanu, false);
            //CanvasGroupOnOf(MainCanvas, true);
        }

        if(FinScript.OnTriggerFin)
        {
            CanvasGroupOnOf(MainCanvas, false);
            ResultCanvasObj.SetActive(true);
            CanvasGroupOnOf(ResultCanvas, true);
            FakeRay.SetActive(true);
        }

        

        //if(mainUI.OnPause)
        //{
        //Ray ray = new Ray(Camera.transform.position, Camera.transform.forward);
        



        // }
    }

    public void ExitPauseManu()//Pause画面→元の状態へ　　　　あとでゲーム自体の停止も加える？
    {

        CanvasGroupOnOf(Pausemanu, false);
        CanvasGroupOnOf(MainCanvas, true);
        //PausemanuObj.SetActive(false);
        //MainCanvasObj.SetActive(true);
        OnPause = false;
        playerVRScript.Move();
        FakeRay.SetActive(false);
    }

    public void ExitResultCanvas()
    {
        CanvasGroupOnOf(MainCanvas, true);
        CanvasGroupOnOf(ResultCanvas, false);

        //ResultCanvasObj.SetActive(false);
        //MainCanvasObj.SetActive(true);

        FakeRay.SetActive(false);

    }

    public void BackTitle()//タイトル画面に戻る
    {
        //SceneManager.LoadScene("TitleScene");
        SceneManager.LoadScene("MainScene");
    }

    public void pushSoundButton()
    {
        CanvasGroupOnOf(Soundmanu, true);
        CanvasGroupOnOf(Pausemanu, false);

        //PausemanuObj.SetActive(false);
        //SoundmanuObj.SetActive(true);
    }
    public void pushContllolButton()
    {
        CanvasGroupOnOf(Contllolmanu, true);
        CanvasGroupOnOf(Pausemanu, false);

        //PausemanuObj.SetActive(false);
        //ContllolmanuObj.SetActive(true);
    }
    public void BackPauseManu1()
    {
        CanvasGroupOnOf(Soundmanu, false);
        CanvasGroupOnOf(Pausemanu, true);

        //PausemanuObj.SetActive(true);
        //SoundmanuObj.SetActive(false);
    }

    public void BackPauseManu2()
    {
        CanvasGroupOnOf(Contllolmanu, false);
        CanvasGroupOnOf(Pausemanu, true);

        //PausemanuObj.SetActive(true);
        //ContllolmanuObj.SetActive(false);

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
