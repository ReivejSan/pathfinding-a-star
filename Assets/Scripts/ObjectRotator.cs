using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    Unit unit;

    [SerializeField]
    GameObject enemyObject;

    [SerializeField]
    private Vector3 _rotation;

    [SerializeField]
    private float _rotationSped;

    void Update()
    {
         enemyObject.transform.Rotate(_rotation * _rotationSped * Time.deltaTime);
    }
}
