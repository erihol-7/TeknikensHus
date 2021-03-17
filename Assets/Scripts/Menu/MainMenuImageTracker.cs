using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;

public class MainMenuImageTracker : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager mTrackedImageManager;

    private bool pressedPlay = false;
    
    //[SerializeField] private ARSession arSession;

    private void Awake()
    {
        pressedPlay = false;
    }

    void OnEnable()
    {
        mTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        mTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            //SceneManager.LoadScene(trackedImage.referenceImage.name);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (pressedPlay)
            {
                  Scene scene = SceneManager.GetActiveScene();
                  if (scene.name != trackedImage.referenceImage.name && trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
                  {
                      //arSession.Reset();
                      SceneManager.LoadScene(trackedImage.referenceImage.name, LoadSceneMode.Single);
                  }  
            }

        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            // not used
            // images does not get removed automatically
        }
    }

    public void PressedPlay()
    {
        pressedPlay = true;
    }
    
    public void UnPressedPlay()
    {
        pressedPlay = false;
    }
}
