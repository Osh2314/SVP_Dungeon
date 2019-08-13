using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject panel_Play_Pause;
    public Slider slider_PlayerHp;
    public Slider slider_CoreHp;
    public Text text_GoldValue;

    public Text text_SelectObjNameValue;
    public Text text_SelectObjPriceValue;
    public Image image_ObjectSprite;
    public Text text_ObjToolTipValue;

    public int nowSaveFileSelectNum;
    public struct NowSelectObjInfo {
        public GameObject nowSelectObj;
        public string nowSelectObjName;
        public int nowSelectObjPrice;
        public string nowSelectObjToolTip;
    }

    public NowSelectObjInfo nowSelectObjInfo;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static UIManager instance;
    
    private void Awake()
    {
        if (UIManager.Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    //메인화면에서 쓸 함수들*****************************************
    public void SetSaveFileNum(int value=0)
    {
        nowSaveFileSelectNum = value;
        if (value == 0)
            Debug.Log("@@@@@@@@nowSaveFIleSelectNum이 0입니다");
    }

    public void LoadInGameScene() {

    }
    
    public void GameExit() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();//프로그램 종료
#endif
    }
    //***************************************************************
    //플레이 화면에서 쓰일 함수들**************************************
    public void GameStart()
    {
        LoadSaveData();
        GameManager.Instance.isGamePlaying = true;

        //씬 변경 함수를 이 함수에 삽입해야한다
    }

    void LoadSaveData() {
        //nowSaveFileSelectNum
    }
    public void GamePause()
    {
        Time.timeScale = 0;
    }

    public void OnGoldChanged()
    {
        text_GoldValue.text = GameManager.Instance.Gold.ToString();
    }
    public void SetSelectObjInfo(GameObject selectObj, string selectObjName, int selectObjPrice,
        string selectObjToolTip) {
        //선택오브젝트 정보 구조체를 업데이트한다.
        nowSelectObjInfo.nowSelectObj = selectObj;
        nowSelectObjInfo.nowSelectObjName = selectObjName;
        nowSelectObjInfo.nowSelectObjPrice = selectObjPrice;
        nowSelectObjInfo.nowSelectObjToolTip = selectObjToolTip;

        //선택오브젝트 정보 패널을 업데이트한다.
        text_SelectObjNameValue.text = selectObjName;
        text_SelectObjPriceValue.text = selectObjPrice.ToString();
        image_ObjectSprite.sprite = selectObj.GetComponent<SpriteRenderer>().sprite;
        text_ObjToolTipValue.text = selectObjToolTip;
}
    //***************************************************************
}
