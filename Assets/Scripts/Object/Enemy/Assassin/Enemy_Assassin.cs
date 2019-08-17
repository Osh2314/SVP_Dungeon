﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Assassin : Enemy
{
    // Start is called before the first frame update
    public GameObject Knife;
    public GameObject Cut;
    public float knifespeed;
    public float cutspeed;

    private bool isThrowing = true;
    public int atkpoint = 1;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override IEnumerator State_Attack()
    {
        state = State.ATTACK;
        atkpoint++;
        while (state == State.ATTACK)
        {
            if (atkpoint >= 2)
            {
                Debug.Log("삭제");
                atkpoint--;
                yield break;
            }
            if (isThrowing == false)
            {
                if (gameObject.transform.position.x <= playerPos.x)
                {
                    GameObject createdObject;
                    Rigidbody2D createdRigid;
                    createdObject = Instantiate(Cut, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z), Quaternion.identity);
                    createdRigid = createdObject.GetComponent<Rigidbody2D>();
                    createdRigid.AddForce(new Vector2(cutspeed, 0));
                }
                else
                {
                    GameObject createdObject;
                    Rigidbody2D createdRigid;
                    createdObject = Instantiate(Cut, new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z), Quaternion.identity);
                    createdRigid = createdObject.GetComponent<Rigidbody2D>();
                    createdRigid.AddForce(new Vector2(cutspeed * -1, 0));
                }
            }
            else
            {
                GameObject createdObject;
                Rigidbody2D createdRigid;
                Vector2 lookDir = playerPos - transform.position;
                lookDir.Normalize();
                createdObject = Instantiate(Knife, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.FromToRotation(Vector3.right, lookDir));
                createdRigid = createdObject.GetComponent<Rigidbody2D>();
                createdRigid.AddForce(lookDir * knifespeed);
            }
                yield return new WaitForSeconds(3f);
        }
        atkpoint--;
        yield break;
    }

public void GetIsThrowing(bool iT)
{
        isThrowing = iT;
}

}

//state = State.ATTACK;
//        atkp++;
//        while (state == State.ATTACK)
//        {
//            if (atkp >= 2)
//            {
//                atkp--;
//                yield break;
//            }
//            GameObject createdObject;
//Rigidbody2D createdRigid;
//Vector2 lookDir = playerPos - transform.position;
//lookDir.Normalize();
//            createdObject = Instantiate(Knife, new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z), Quaternion.FromToRotation(Vector3.right, lookDir));
//            createdRigid = createdObject.GetComponent<Rigidbody2D>();
//            createdRigid.AddForce(lookDir* knifespeed);
//            yield return new WaitForSeconds(3f);
//        }
//        atkp--;
//        yield break;
//    }
