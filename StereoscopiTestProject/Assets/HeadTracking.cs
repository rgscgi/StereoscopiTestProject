using UnityEngine;
using System.Collections;

public class HeadTracking : MonoBehaviour {

    private Gyroscope gyro;
    private Quaternion initialRotation;

	// Use this for initialization
	void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if(SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.Log("No Gyro Support");
        }

        initialRotation = transform.rotation;
	
	}
	
	// Update is called once per frame
	void Update (){
        if(SystemInfo.supportsGyroscope)
        {
            if (Input.touchCount > 0)
            {
                transform.rotation = initialRotation;
            }
        }

        Vector3 orientationSpeed = Input.gyro.rotationRateUnbiased * Time.deltaTime;
        transform.rotation = transform.rotation * Quaternion.Euler( orientationSpeed.x , orientationSpeed.y,orientationSpeed.z );
	    
	}
}
