using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpowner : MonoBehaviour
{

    [Header("적의 스폰 위치 오브젝트")]
    public GameObject spownPlace;
    [Header("스폰간격 시간")]
    public float spwonIntervalTime = 2.5f;
    [Header("어떤 적을 스폰할지 넣는다")]
    public GameObject[] enemys;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpownWithIntervalTime(spwonIntervalTime));
    }

    IEnumerator SpownWithIntervalTime(float time_Interval) {
        while (GameManager.Instance.isGamePlaying != false)
        {
            Instantiate(enemys[Random.Range(0, enemys.Length)], spownPlace.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(time_Interval);
            
        }
        yield break;
    }
}
