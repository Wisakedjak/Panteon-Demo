using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swerveMovement : MonoBehaviour
{
    private swerveInputSystem _swerveInputSystem;
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;
    public float clampAmount = 2f;

    private void Awake()
    {
        _swerveInputSystem = GetComponent<swerveInputSystem>();
    }

    private void Update()
    {
        float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
        transform.Translate(swerveAmount, 0, .0365f);
        transform.position= new Vector3(Mathf.Clamp(transform.position.x, -clampAmount, clampAmount),
           transform.position.y, transform.position.z);
    }
}