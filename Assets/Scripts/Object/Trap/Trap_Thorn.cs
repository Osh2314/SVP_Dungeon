﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_Thorn : MonoBehaviour
{
    public int damage = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.Hp -= damage;
        }
    }

}