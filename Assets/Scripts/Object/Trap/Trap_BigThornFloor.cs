using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_BigThornFloor : MonoBehaviour
{
    GameObject Thron1;
    GameObject Thron2;
    GameObject Thron3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Thorn()
    {
        while(true)
        {
            Thron1.gameObject.SetActive(false);
            Thron2.gameObject.SetActive(false);
            Thron3.gameObject.SetActive(false);
            yield return new WaitForSeconds(3f);
            Thron1.gameObject.SetActive(true);
            Thron2.gameObject.SetActive(true);
            Thron3.gameObject.SetActive(true);
            yield return new WaitForSeconds(3f);
        }
    }
}
