using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Assassin : Enemy
{
    // Start is called before the first frame update
    public GameObject Knife;
    public float knifespeed;

    Enemy_Assassin_Knife knife;
    public int atkpoint = 0;

    // Start is called before the first frame update
    new void Start()
    {
        knife = GetComponent<Enemy_Assassin_Knife>();
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
                atkpoint--;
                yield break;
            }
            GameObject createdObject;
            Rigidbody2D createdRigid;
            Vector2 lookDir = playerPos - transform.position;
            lookDir.Normalize();
            createdObject = Instantiate(Knife, new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z), Quaternion.FromToRotation(Vector3.right, lookDir));
            createdRigid = createdObject.GetComponent<Rigidbody2D>();
            createdRigid.AddForce(lookDir * knifespeed);
            yield return new WaitForSeconds(3f);
        }
        atkpoint--;
        yield break;
    }
}
