using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionScript : MonoBehaviour
{

    [SerializeField] private GameObject worldOrigin;
    
    // Start is called before the first frame update
    void Start()
    {
        if (worldOrigin == null)
        {
            worldOrigin = GameObject.FindGameObjectWithTag("WorldOrigin");
        }
        
        var itemObject = transform;
        var position = itemObject.position;      // get position of object
        itemObject.parent = worldOrigin.transform;   // set object as a child of worldOrigin
        itemObject.localPosition = position;         // Set position of object relative worldOrigin
    }

}
