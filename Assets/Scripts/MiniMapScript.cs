using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{

    public float speed = 100f;
    public Transform camera;
    // Start is called before the first frame update
    private void LateUpdate()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);

    }
}
