using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject target;
    public float lerpConstant=0.7f;
    public float min_x=-10;
    public float max_x=10;
    private Vector3 offSet;

    // Start is called before the first frame update
    void Start()
    {

        
        target = GameManager.Instance.player.gameObject;
        offSet = new Vector3(target.transform.position.x - transform.position.x, 0, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = Vector3.Lerp(new Vector3(target.transform.position.x - offSet.x, 0, transform.position.z), transform.position, 0.7f);
        newPos.x = Mathf.Clamp(newPos.x, min_x, max_x);
        transform.position = newPos;
    }
}
