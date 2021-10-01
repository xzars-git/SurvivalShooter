﻿using UnityEngine;

public class CameraFollow : UnityEngine.MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    Vector3 offset;

    private void Start()
    {
        //Mendapatkan offset antara target dan camera 
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        //Mendapatkan posisi untuk camera
        Vector3 targetCamPos = target.position + offset;

        //set posisi camera dengan smoothing
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
