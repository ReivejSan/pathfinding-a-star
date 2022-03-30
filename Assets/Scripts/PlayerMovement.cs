using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField]
    //Transform cameraRotator = null;

    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField]
    private float rollingSpeed = 1f;
    
    Vector3 targetPosition;
    //Vector3 startPosition;
    //startPosition = transform.position;

    public CharacterController controller;

    public float movementSpeed = 1f;
    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;

    public bool colliding;

    void Start()
    {
       rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        /*Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);

        if(direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * movementSpeed * Time.deltaTime);
            targetPosition = transform.position + cameraRotator.transform.forward;
        }*/

        //targetPosition = transform.position + cameraRotator.transform.forward;
    }

    void LateUpdate()
    {
        rb.AddForce(new Vector3(horizontalInput, 0.0f, verticalInput) * rollingSpeed);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.Instance.isDead = true;
        }
    }
}
