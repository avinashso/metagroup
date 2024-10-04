using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShapeX : MonoBehaviour
{
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        transform.Rotate(rotateSpeed, 0, 0);
    }
}
