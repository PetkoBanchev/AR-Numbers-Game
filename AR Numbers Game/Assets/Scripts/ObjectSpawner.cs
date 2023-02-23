using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ObjectSpawner : MonoBehaviour
{
    private ARRaycastManager raycastManager;
    private GameObject spawnedObject;

    [SerializeField]
    private GameObject prefab; //to be renamed

    private static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    private bool isObjectLocked = false;

    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        GetComponent<LockObjectPosition>().OnObjectLockedStateChanged += ToggleObjectLockState; //subscribing to the event
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!TryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }
        if(raycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon) && !isObjectLocked)
        {
            var hitPose = s_Hits[0].pose;
            if(spawnedObject == null)
            {
                spawnedObject = Instantiate(prefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation= hitPose.rotation;
            }
        }
    }

    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;   
    }

    private void ToggleObjectLockState(bool lockState)
    {
        isObjectLocked = lockState;
    }
}
