using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3DController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 300f;
    [SerializeField] float runSpeed = 400f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float rotateSpeed = 5f;
    public Animator animator;
    CharacterController characterController;

    float moveAmount;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        float speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && moveAmount > 0)
        {
            speed = runSpeed;
            moveAmount += 1;
        }

        characterController.SimpleMove(moveDirection * speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveAmount += 1;
        }
        animator.SetFloat("Speed", moveAmount);

        if (moveAmount != 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("adfasdfasdf");
            animator.SetTrigger("Attack");
        }
    }
}
