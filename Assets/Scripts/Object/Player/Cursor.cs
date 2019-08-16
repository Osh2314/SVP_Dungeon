using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//마우스를 따라다닐 sprite컴포넌트가 있는 오브젝트 : Cursor
public class Cursor : MonoBehaviour
{
    public Sprite idle_Crosshair;
    public enum State { Idle, Install}
    public State state = State.Idle;
    public Vector3 MouseConvertedpoint {
        get {
            return mouseConvertedpoint;
        }
        set {
            mouseConvertedpoint = value;
        }
    }
    private Vector3 mouseConvertedpoint;

    private GameObject nowInstallObjInfo;
    private bool canInstall = true;
    private SpriteRenderer spriteRend;
    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        MouseConvertedpoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        transform.position = MouseConvertedpoint;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (state != State.Install)
            return;
        Debug.Log(other.gameObject.name + " : touched!!!");
        if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Floor"
            || other.gameObject.tag == "Player")
        {
            spriteRend.color = new Color(255, 0, 0);
            canInstall = false;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (state != State.Install)
            return;
        if (other.gameObject.tag == "Platform" || other.gameObject.tag == "Floor"
            || other.gameObject.tag == "Player")
            spriteRend.color = new Color(255, 255, 255);
        canInstall = true;
    }

    /// <summary>
    /// 조건에 맞으면 들고 있는 오브젝트를 설치한다.
    /// </summary>
    /// <returns></returns>
    public bool tryInstallObj()
    {
        if (canInstall == true &&
            GameManager.Instance.Gold - UIManager.Instance.nowSelectObjInfo.nowSelectObjPrice >= 0)
        {
            GameManager.Instance.Gold -= UIManager.Instance.nowSelectObjInfo.nowSelectObjPrice;
            Instantiate(nowInstallObjInfo, transform.position, Quaternion.identity, GameManager.Instance.platformData.transform);
        }

        return canInstall == true && GameManager.Instance.Gold - UIManager.Instance.nowSelectObjInfo.nowSelectObjPrice >= 0;
    }

    public IEnumerator State_Idle() {
        state = State.Idle;
        spriteRend.sprite = idle_Crosshair;
        spriteRend.color = new Color(255, 255, 255);
        Destroy(GetComponent<Collider2D>());
        Collider2D nowColl = gameObject.AddComponent<BoxCollider2D>();
        nowColl.isTrigger = true;
        while (state == State.Idle) {
            yield return null;
        }
    }
    public IEnumerator State_Install(GameObject objInfo) {
        state = State.Install;
        
        nowInstallObjInfo = objInfo;
        spriteRend.sprite = objInfo.GetComponent<SpriteRenderer>().sprite;
        Collider2D nowColl=CopyComponent<Collider2D>(objInfo.GetComponent<Collider2D>(), gameObject);
        nowColl.isTrigger = true;
        while (state == State.Install) {
            
            yield return null;
        }
        Destroy(nowColl);

    }

   

    public T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = destination.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }
    //public void setTrueInstallMode()
    //{
    //    isInstallMode = true;
    //    mouseFollowObj = Instantiate(objInfo, new Vector3(), Quaternion.identity);
    //    if (mouseFollowObj.GetComponent<InstallObj_Rendering>() == null)
    //    {
    //        Debug.Log("Add" + Time.time);
    //        mouseFollowObj.AddComponent<InstallObj_Rendering>();
    //    }
    //    mouseFollowObj.SetActive(true);
    //}
    //public void setFalseInstallMode()
    //{
    //    isInstallMode = false;
    //    Destroy(mouseFollowObj);
    //}

    //private void tryInstallObj()
    //{
    //    if (mouseFollowObj.GetComponent<InstallObj_Rendering>().canInstall == true &&
    //        GameManager.Instance.Gold - UIManager.Instance.nowSelectObjInfo.nowSelectObjPrice >= 0)
    //    {
    //        GameManager.Instance.Gold -= UIManager.Instance.nowSelectObjInfo.nowSelectObjPrice;
    //        GameObject installedObj = Instantiate(mouseFollowObj, mouseFollowObj.transform.position, Quaternion.identity);
    //        Destroy(installedObj.GetComponent<InstallObj_Rendering>());
    //        installedObj.transform.parent = GameManager.Instance.platformData.transform;
    //    }

    //}
}
