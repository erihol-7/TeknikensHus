using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackerScript : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager mTrackedImageManager;
    
    [SerializeField]
    private GameObject worldOrigin;
    
    [SerializeField] 
    ARAnchorManager anchorManager;
    
    [SerializeField] 
    private ARCameraManager arCamera;

    
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
            //Delay anchor to stabilize image tracking
            StartCoroutine(ExecuteAfterTime(1, trackedImage)); 
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage); // Used for debug
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            // not used
            // images does not get removed automatically
        }
    }
    
    IEnumerator ExecuteAfterTime(float time,ARTrackedImage trackedImage)
    {
        //Wait "time" and then set an anchor at image location
        yield return new WaitForSeconds(time);
        // AnchorContent to image pos (Quaternion.FromToRotation is a new rotation test)
        AnchorContent(trackedImage.transform, worldOrigin.transform);
    }

    private void AnchorContent(Transform image, Transform worldOrigin)
    {
        // Fix Y rotation problem
        
        // Create a new anchor.
        var anchor = anchorManager.AddAnchor(new Pose(image.position, image.rotation)); // Set new anchor at image
        
        // Parent worldOrigin to it.
        worldOrigin.parent = anchor.transform;
        worldOrigin.localPosition = new Vector3(0f,0f,0f);
        //worldOrigin.rotation = arCamera.transform.rotation;
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        //Debug.Log($"worldOrigin is : {position}");
        //Debug.Log($"Tracking state: {trackedImage.trackingState}");
        Debug.Log($"rot is : {arCamera.transform.rotation}");
    }
    
}
