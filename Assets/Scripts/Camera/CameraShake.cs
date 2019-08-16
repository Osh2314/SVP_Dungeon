using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public IEnumerator cameraVibration(float duration){
	while(duration>0){
	
	
	    duration-=Time.deltaTime;
	}
	yield break;
    }
}
