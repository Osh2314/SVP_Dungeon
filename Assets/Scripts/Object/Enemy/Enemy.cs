using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
    public float spinforce = 10000;
    public int dropGoldValue = 10;
    public enum State { IDLE, MOVE, STUN, ATTACK, DEAD};
    public State state = State.IDLE;

    private Rigidbody2D rigid;
    
    // Start is called before the first frame update
    protected void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(State_Idle());
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public IEnumerator State_Idle() {
        state = State.IDLE;

        //ThisAnimator.SetTrigger((int)State.IDLE);

        while (state == State.IDLE) {
            //플레이어
            if (GameManager.Instance.isGamePlaying == true) {
                StartCoroutine(State_Move());
                yield break;
            }
            //Debug.Log(Time.realtimeSinceStartup + " || " + "현재 IDLE상태");
            yield return null;
        }
        yield break;
    }

    public IEnumerator State_Move()
    {
        state = State.MOVE;
        Vector3 dir = GameManager.Instance.core.transform.position - transform.position;
        dir.Normalize();
        //ThisAnimator.SetTrigger((int)State.IDLE);

        while (state == State.MOVE)
        {
            //플레이어
            if (GameManager.Instance.isGamePlaying == true)
            {
                transform.Translate(new Vector3((dir*speed * Time.deltaTime).x, 0, 0));
                //if()
            }
         //   Debug.Log(Time.realtimeSinceStartup + " || " + "현재 MOVE상태");
            yield return null;
        }
        yield break;
    }

    public virtual IEnumerator State_Attack()
    {
        yield break;
    }

    public IEnumerator State_Dead()
    {
        rigid.constraints = RigidbodyConstraints2D.None;
        rigid.AddForce(new Vector3(-500, 600, spinforce));

        float duration = 2.0f;
        while (duration > 0) {
            duration -= Time.deltaTime;

            transform.localEulerAngles += new Vector3(0, 0, 300 * Time.deltaTime);
            yield return null;
        }
        GameManager.Instance.Gold += dropGoldValue;
        Destroy(gameObject);
        yield break;
    }

}
