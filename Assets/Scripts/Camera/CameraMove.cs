using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject target;
    public float lerpConstant=0.7f;

    private Vector3 offSet;

    // Start is called before the first frame update
    void Start()
    {
        offSet = new Vector3(target.transform.position.x - transform.position.x, 0, 0);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(new Vector3(target.transform.position.x - offSet.x, 0, transform.position.z), transform.position, 0.7f);
    }
}
