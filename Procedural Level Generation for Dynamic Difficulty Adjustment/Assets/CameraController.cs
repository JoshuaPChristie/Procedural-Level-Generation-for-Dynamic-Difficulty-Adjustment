using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform camTransform;
    public Transform camTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camTransform.position = new Vector3(camTarget.position.x, camTransform.position.y, camTransform.position.z);
    }
}
