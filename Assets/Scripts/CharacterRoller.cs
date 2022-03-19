using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoller : MonoBehaviour
{
    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;

    [SerializeField]
    private float speed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void LateUpdate()
    {
        rb.AddForce(new Vector3(horizontalInput, 0.0f, verticalInput) * speed);
    }
}
