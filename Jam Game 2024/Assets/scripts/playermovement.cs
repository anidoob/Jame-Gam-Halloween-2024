using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class playermovement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    public Vector3 moveDirection = Vector3.zero;
    public bool isRunning;
    public bool isMoving;
    private float rotationX = 0;
    private CharacterController characterController;

    public int maxTemp = 20;
    public int currentTemp;
    public TemperatureScript temp;
    
    private headbob headbob;
    Animator animator;
    private bool canMove = true;

    [SerializeField]private GameManager gameManager;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = GetComponent<Animator>();
        headbob = GetComponent<headbob>();
        currentTemp = maxTemp;
        temp.setTempMax(maxTemp);
        StartCoroutine(tempOverTime());
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

       if(moveDirection != Vector3.zero)
            isMoving = true;
        if (moveDirection == Vector3.zero)
            isMoving = false;

        if (isMoving)
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isWalk", true);
            if (isRunning)
            {
                animator.SetBool("isRun", true);
                animator.SetBool("isWalk", true);
            }

        }
        else
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isWalk", false);
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.R) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;

        }
        else
        {
            characterController.height = defaultHeight;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    private IEnumerator tempOverTime()
    {
        while (currentTemp > 0)
        {
            yield return new WaitForSeconds(5);
            currentTemp--;
            temp.SetTemp(currentTemp);

            if(currentTemp == 0)
            {
                gameManager.EndGame();
            }
            Debug.Log("Temp: " + currentTemp);
        }
    }
}