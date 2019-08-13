using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShop_ObjectBtn : MonoBehaviour
{
    public GameObject obj;
    [Header("블록의 이름")]
    public string objName = "아무개";
    [Header("블록의 가격")]
    public int objPrice=0;
    [Header("블록의 설명")]
    public string objToolTip = "";

    public void SelectObject() {
        Player player = GameManager.Instance.player.GetComponent<Player>();
        player.setTrueInstallMode(obj);

        UIManager.Instance.SetSelectObjInfo(obj, objName, objPrice, objToolTip);

    }

}
