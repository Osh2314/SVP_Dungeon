﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float origin_JumpForce;
    public bool cJump = true;

    public enum STATE { IDLE, MOVE, STOP, ATTACK, DEAD };
    public STATE state = STATE.IDLE;


    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
            UIManager.Instance.slider_PlayerHp.value = hp;
            if (Hp <= 0)
            {
                event_Death();
            }
            Debug.Log(gameObject.name + " hp : " + Hp);
        }
    }

    private int hp = 10;
    public bool isInstallMode = false;
    
    //마우스를 따라다닐 sprite컴포넌트가 있는 오브젝트
    private GameObject mouseFollowObj;
    //현재 셀렉중인 블록의 가격
    private bool isSeeRight = true;
    private float jumpForce;
    
    private Rigidbody2D rigid;
    
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        UIManager.Instance.slider_PlayerHp.maxValue = hp;
        UIManager.Instance.slider_PlayerHp.value = hp;
        StartCoroutine(STATE_IDLE());
    }

    void Update()
    {
        if (GameManager.Instance.isGamePlaying == false)
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
       
        transform.position += new Vector3(h * speed * Time.deltaTime, 0, 0);


        if (Input.GetKeyDown(KeyCode.Space) && cJump == true)
        {
            rigid.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            cJump = false;
        }
        if (h >0) // 1이 오른쪽 -1이 왼쪽
        {
            isSeeRight = true;
            transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        if(h<0)
        {
            isSeeRight = false;
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (isInstallMode == true)
        {
            Vector3 mouseConvertedpoint = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            mouseFollowObj.transform.position = mouseConvertedpoint;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //설치를 구현하는 함수
                tryInstallObj();

            }
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                setFalseInstallMode();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            cJump = true;
            jumpForce = origin_JumpForce;
        }
        if (collision.gameObject.tag == "Floor")
        {
            cJump = true;
            jumpForce = origin_JumpForce * 2;
        }
    }

    public bool GetDirection() // Direction == 방향
    {
        return isSeeRight;
    }

    IEnumerator STATE_IDLE()
    {
        state = STATE.IDLE;

        //ThisAnimator.SetTrigger((int)State.IDLE);

        while (state == STATE.IDLE)
        {
            //플레이어
            if (GameManager.Instance.isGamePlaying == true)
            {
                StartCoroutine(STATE_MOVE());
                yield break;
            }
            //Debug.Log(Time.realtimeSinceStartup + " || " + "현재 IDLE상태");
            yield return null;
        }
        yield break;
    }

    IEnumerator STATE_MOVE()
    {
        state = STATE.MOVE;
        //ThisAnimator.SetTrigger((int)State.IDLE);

        while (state == STATE.MOVE)
        {
            //플레이어
            if (GameManager.Instance.isGamePlaying == true)
            {
            }
            //   Debug.Log(Time.realtimeSinceStartup + " || " + "현재 MOVE상태");
            yield return null;
        }
        yield break;
    }

    public void setTrueInstallMode(GameObject objInfo) {
        isInstallMode = true;
        mouseFollowObj = Instantiate(objInfo, new Vector3(), Quaternion.identity);
        if (mouseFollowObj.GetComponent<InstallObj_Rendering>() == null)
        {
            Debug.Log("Add" + Time.time);
            mouseFollowObj.AddComponent<InstallObj_Rendering>();
        }
        mouseFollowObj.SetActive(true);
    }
    public void setFalseInstallMode()
    {
        isInstallMode = false;
        Destroy(mouseFollowObj);
    }

    private void tryInstallObj() {
        if (mouseFollowObj.GetComponent<InstallObj_Rendering>().canInstall &&
            GameManager.Instance.Gold - UIManager.Instance.nowSelectObjInfo.nowSelectObjPrice>=0)
        {
            GameManager.Instance.Gold -= UIManager.Instance.nowSelectObjInfo.nowSelectObjPrice;
            GameObject installedObj = Instantiate(mouseFollowObj, mouseFollowObj.transform.position, Quaternion.identity);
            Destroy(installedObj.GetComponent<InstallObj_Rendering>());
            installedObj.transform.parent = GameManager.Instance.platformData.transform;
        }
    }
    private void event_Death()
    {
        Destroy(gameObject);
    }
}
