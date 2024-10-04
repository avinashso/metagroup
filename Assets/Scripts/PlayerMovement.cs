using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float jumpButtonGracePeriod;
    public GameObject cubeText;
    public GameObject sphereText;
    public GameObject cylinderText;
    public GameObject rectangularText;

    Rigidbody rb;

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    private void Awake()
    {
        rb= GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
       
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -5f)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       //if (other.gameObject.tag == "Cube")
       // {
       //     other.gameObject.SetActive(false);
       //     cubeText.SetActive(true);
       // }
       // else if (other.gameObject.tag == "Sphere")
       // {
       //     other.gameObject.SetActive(false);
       //     sphereText.SetActive(true);
       // }


        switch (other.gameObject.tag)
        {
            case "Cube":
                other.gameObject.SetActive(false);
                cubeText.SetActive(true);
                break;
            case "Sphere":
                other.gameObject.SetActive(false);
                sphereText.SetActive(true);
                break;
            case "Cylinder":
                other.gameObject.SetActive(false);
                cylinderText.SetActive(true);
                break;
            case "Rectangular":
                other.gameObject.SetActive(false);
                rectangularText.SetActive(true);
                break;


        }


    }
}

