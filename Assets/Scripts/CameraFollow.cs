﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraFollow : MonoBehaviour {

    [SerializeField] Transform target;
    [SerializeField] float smoothing = 5f;

    Vector3 offset;

    private void Awake()
    {
        Assert.IsNotNull(target);
    }

    void Start ()
    {
        offset = transform.position - target.position;
	}
	
	void Update ()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}
