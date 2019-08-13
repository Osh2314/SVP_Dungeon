using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AttackObject: MonoBehaviour
{
    public int damage = 5;
    public bool canAttack = false;
    [Header("카메라 밖으로 나가면 오브젝트를 삭제 할것인가?")]
    public bool willInvisibleDestroy = false;
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
        if (collision.gameObject.tag == "Player" && canAttack == true)
        {
            Debug.Log("Hit!");
            Player player = collision.gameObject.GetComponent<Player>();
            player.Hp -= damage;
            canAttack = false;
        }
        if (collision.gameObject.tag == "Core" && canAttack == true)
        {
            Debug.Log("Hit!");
            Core core = collision.gameObject.GetComponent<Core>();
            core.Hp -= damage;
            canAttack = false;
        }

    }
    void OnBecameInvisible()
    {
        if(willInvisibleDestroy==true)
        Destroy(gameObject);
    }
}
