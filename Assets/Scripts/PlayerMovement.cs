using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Stats
    [SerializeField] private Stats _stats;

    // cam movement
    [SerializeField] private Transform _eyes;
    [SerializeField] private float _sensitivity;
    [Range(-90f, 0f)]
    [SerializeField] private float _camLimitMin;
    [Range(0, 90f)]
    [SerializeField] private float _camLimitMax;
    private float _camAngle = 0.0f;

    // Movement
    private Rigidbody _rb;

    // Jumping

    // Interact
    [SerializeField] private KeyCode _interactKey;
    [SerializeField] private float _interactRange;

    // Ability
    [SerializeField] private KeyCode _abilityKey;
    [SerializeField] private Ability _ability;

    public Rigidbody Rb 
    { 
        get => _rb; 
        private set => _rb = value;
    }

    private void Start()
    {
        Rb = GetComponent<Rigidbody>();

        Cursor.visible = false;
    }
    private void Update()
    {
        RotateEyes();
        RotateBody();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryJump();
        }

        if (Input.GetKeyDown(_interactKey))
        {
            TryInteract();
        }

        if (Input.GetKeyDown(_abilityKey))
        {
            _ability.Use(this);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void RotateEyes()
    {
        float yMouse = Input.GetAxis("Mouse Y") * _sensitivity * Time.deltaTime;
        _camAngle -= yMouse;
        _camAngle = Mathf.Clamp(_camAngle, _camLimitMin, _camLimitMax);
        _eyes.localRotation = Quaternion.Euler(_camAngle, 0, 0);
    }

    private void RotateBody()
    {
        float xMouse = Input.GetAxis("Mouse X") * _sensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * xMouse);
    }

    private void Move()
    {
        float xDir = Input.GetAxis("Horizontal");
        float zDir = Input.GetAxis("Vertical");

        Vector3 dir = (transform.right * xDir + transform.forward * zDir);

        Rb.velocity = new Vector3(0, Rb.velocity.y, 0) + dir.normalized * _stats.moveSpeed;
    }

    private void TryInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(_eyes.position, _eyes.forward, out hit, _interactRange))
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    private void TryJump()
    {
        if (IsGrounded())
        {
            Jump(_stats.jumpHeight);
        }
    }

    private void Jump(float jumpForce)
    {
        Rb.velocity = new Vector3(Rb.velocity.x, 0, Rb.velocity.z);
        Rb.AddForce(transform.up * _stats.jumpHeight, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, -transform.up, out hit, 1.1f);
    }
}