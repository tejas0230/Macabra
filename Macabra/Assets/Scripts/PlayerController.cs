using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    public bool CanRotateCam { get;set; } = true;
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchingAnimation && characterController.isGrounded;
    [Header("Functional Options")]
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool useFootSteps = true;


    [Header("Controls")]
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float gravity = 30f;
    [SerializeField] private float crouchSpeed = 1.5f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80f;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 1.8f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchingAnimation;


    private Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    [Header("Footstep Parameters")]
    [SerializeField] private float baseSpeed = 0.5f;
    [SerializeField] private AudioSource footStepAudioSource = default;
    [SerializeField] private AudioClip[] woodClips = default;
    [SerializeField] private AudioClip[] grassClips = default;
    [SerializeField] private LayerMask groundMask;
    private float footStepTimer = 0;
    private float GetCurrentOffeset => baseSpeed;
    private void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       if(CanMove)
        {
            HandleMovementInput();
            HandleMouseInput();

            if (canCrouch)
                HandleCrouch();
            if (useFootSteps)
                HandleFootSteps();
            ApplyFinalMovement();
        }
    }

    private void HandleFootSteps()
    {
        if (!characterController.isGrounded)
            return;
        if (currentInput == Vector2.zero)
            return;

        footStepTimer -= Time.deltaTime;
        if(footStepTimer<=0)
        {
            if(Physics.Raycast(playerCamera.transform.position,Vector3.down,out RaycastHit hit,3))
            {
                switch(hit.collider.tag)
                {
                    case "Wood":
                        Debug.Log("Wood");
                        footStepAudioSource.PlayOneShot(woodClips[UnityEngine.Random.Range(0, woodClips.Length - 1)]);          
                    break;

                    case "Grass":
                        footStepAudioSource.PlayOneShot(grassClips[UnityEngine.Random.Range(0, grassClips.Length - 1)]);
                        break;

                    default:
                        footStepAudioSource.PlayOneShot(woodClips[UnityEngine.Random.Range(0, woodClips.Length - 1)]);
                        break;
                }
            }
            footStepTimer = GetCurrentOffeset;
        }
    }

    private void ApplyFinalMovement()
    {
        if(!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleMouseInput()
    {
        if(CanRotateCam)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
            rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
        }
        
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2((isCrouching?crouchSpeed:walkSpeed)* Input.GetAxis("Vertical"), (isCrouching ? crouchSpeed : walkSpeed) * Input.GetAxis("Horizontal"));
        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;

    }

    private void HandleCrouch()
    {
        if (ShouldCrouch)
            StartCoroutine(CrouchStand());
    }

    private IEnumerator CrouchStand()
    {
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1))
            yield break;

        duringCrouchingAnimation = true;
        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while(timeElapsed<timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;
        duringCrouchingAnimation = false;
    }
}
