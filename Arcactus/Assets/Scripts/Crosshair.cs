﻿using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

    public OVRCameraRig CameraFacing;
    private Vector3 originalScale;

    // Use this for initialization
    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        float distance;
        if (Physics.Raycast(new Ray(CameraFacing.transform.position,
                                     CameraFacing.transform.rotation * Vector3.forward),
                             out hit))
        {
            distance = hit.distance;
        }
        else
        {
            distance = CameraFacing.rightEyeCamera.farClipPlane * 0.95f;
        }
        transform.position = CameraFacing.transform.position +
            CameraFacing.transform.rotation * Vector3.forward * distance;
        transform.LookAt(CameraFacing.transform.position);
        transform.Rotate(0.0f, 180.0f, 0.0f);
        if (distance < 10.0f)
        {
            distance *= 1 + 5 * Mathf.Exp(-distance);
        }
        transform.localScale = originalScale * distance;
    }
}
