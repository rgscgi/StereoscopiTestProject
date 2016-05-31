// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class HitIt : MonoBehaviour, IGvrGazeResponder
{
    private bool Gaze;
    public Transform target;

    void Start()
    {        
        SetGazedAt(false);            
    }

    void LateUpdate()
    {
        GvrViewer.Instance.UpdateState();
        if (GvrViewer.Instance.BackButtonPressed)
        {
            Application.Quit();
        }
    }

    public void Update()
    {        
        if (Input.GetButtonDown("Fire1"))
        { 
            Ray ray = new Ray(transform.position, Camera.main.transform.forward);
            OnLookStateAction(ray);
        }    
     }

    public void SetGazedAt(bool gazedAt)
    {
                Debug.Log("The gazed at is: "+ gazedAt);
                //GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.blue;
    }   

    public void ToggleVRMode()
    {
        GvrViewer.Instance.VRModeEnabled = !GvrViewer.Instance.VRModeEnabled;
    }

    public void ToggleDistortionCorrection()
    {
        switch (GvrViewer.Instance.DistortionCorrection)
        {
            case GvrViewer.DistortionCorrectionMethod.Unity:
                GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.Native;
                break;
            case GvrViewer.DistortionCorrectionMethod.Native:
                GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.None;
                break;
            case GvrViewer.DistortionCorrectionMethod.None:
            default:
                GvrViewer.Instance.DistortionCorrection = GvrViewer.DistortionCorrectionMethod.Unity;
                break;
        }
    }

    public void ToggleDirectRender()
    {
        GvrViewer.Controller.directRender = !GvrViewer.Controller.directRender;
    }

    void OnLookStateAction(Ray rayHit)
    {
        Handheld.Vibrate();
        target.GetComponent<Rigidbody>().AddForceAtPosition(rayHit.direction * 10, rayHit.origin, ForceMode.Impulse);
    }

    //public void OnLookStateAction()
    //{
    //    Debug.Log("I click it");
    //    RaycastHit ray;
    //    Ray RayDirection = new Ray(transform.position,transform.forward);
    //    if( Physics.Raycast(RayDirection,out ray, sightlength)
    //        { 

    //    }
    //    GetComponent<Rigidbody>().AddForceAtPosition(ray.normal * -5, new Vector3( 0f , 0f , 0f) , ForceMode.Impulse);
    //}

    #region IGvrGazeResponder implementation

    /// Called when the user is looking on a GameObject with this script,
    /// as long as it is set to an appropriate layer (see GvrGaze).
    public void OnGazeEnter()
    {
        SetGazedAt(true);
    }

    /// Called when the user stops looking on the GameObject, after OnGazeEnter
    /// was already called.
    public void OnGazeExit()
    {
        SetGazedAt(false);
    }

    /// Called when the viewer's trigger is used, between OnGazeEnter and OnGazeExit.
    public void OnGazeTrigger()
    {
     /*   print("trigger");
        if (Input.GetButtonDown("Fire1"))
        {*/
            print("work it.");
            Ray ray = new Ray(transform.position, Camera.main.transform.forward);
            OnLookStateAction(ray);
       // }    
    }

    #endregion
}
