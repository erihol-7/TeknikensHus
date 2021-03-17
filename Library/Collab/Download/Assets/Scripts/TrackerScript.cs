using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.SceneManagement;

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
            Scene scene = SceneManager.GetActiveScene();
            Debug.Log(scene.name);
            if (scene.name != trackedImage.referenceImage.name && trackedImage.trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                SceneManager.LoadScene(trackedImage.referenceImage.name);

            }
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
        AnchorContent(trackedImage.transform);
    }

    private void AnchorContent(Transform image)
    {
        // Create a new anchor.
        var anchor = anchorManager.AddAnchor(new Pose(image.position, Quaternion.identity)); // Set new anchor at image

        // Parent worldOrigin to it and center.
        worldOrigin.transform.parent = anchor.transform;
        worldOrigin.transform.localPosition = new Vector3(0f, 0f, 0f);

        // Rotate towards camera
        var angle = new Vector3(0, -Vector3.SignedAngle(worldOrigin.transform.position - arCamera.transform.position, worldOrigin.transform.forward, worldOrigin.transform.up) + worldOrigin.transform.rotation.y, 0);
        worldOrigin.transform.rotation = Quaternion.Euler(angle);
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        //Debug.Log($"worldOrigin is : {position}");
        //Debug.Log($"Tracking state: {trackedImage.trackingState}");
        Debug.Log($"rot is : {arCamera.transform.rotation}");
    }
    
}
