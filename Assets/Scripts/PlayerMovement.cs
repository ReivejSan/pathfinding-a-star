using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Transform cameraRotator = null;

    Vector3 targetPosition;
    //Vector3 startPosition;
    //startPosition = transform.position;

    public CharacterController controller;

    public float movementSpeed = 5f;
    public float turnSmoothTime = .1f;
    float turnSmoothVelocity;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if(direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            controller.Move(direction * movementSpeed * Time.deltaTime);
            targetPosition = transform.position + cameraRotator.transform.forward;
        }

        //targetPosition = transform.position + cameraRotator.transform.forward;
    }
}
