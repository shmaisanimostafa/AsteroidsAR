using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private Transform _myTransform;

    void Start()
    {
        // Store the reference of the GameObject's transform
        _myTransform = transform;
    }

    void Update()
    {
        // Increment the position Vector3
        _myTransform.position += _myTransform.forward * (Time.deltaTime * _moveSpeed);
    }
}
