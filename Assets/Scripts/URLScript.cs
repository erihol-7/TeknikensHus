using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLScript : MonoBehaviour
{
public string URL;
public void Open()
{
    Application.OpenURL(URL);
}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
