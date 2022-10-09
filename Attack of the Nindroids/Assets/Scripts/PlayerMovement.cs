using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("Standard Movement Variables")]
    [SerializeField]
    private float _forwardForce;
    [SerializeField]
    private float _velocityClamp;
    [SerializeField]
    private Rigidbody _rb;

    [Header("Crouch Variables")]
    [SerializeField]
    private CapsuleCollider _collider;
    [SerializeField]
    private float _originalColliderHeight;
    [SerializeField]
    private float _originalColliderCenterY;
    [SerializeField]
    private float _crouchColliderHeight;
    [SerializeField]
    private float _crouchColliderCenterY;
    private bool _isCrouching = false;

    [Header("Jump Variables")]
    [SerializeField]
    private bool _isGrounded = true;
    [SerializeField]
    private bool _canJump = false;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private float _addedGravity;

    [Header("Animator Variables")]
    [SerializeField]
    private Animator _animator;

    // Unity actions
    public static Action playerDead;
    public static Action playerWin;
    public static Action<float> playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Get components for the reference variables
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        JumpInput();

        CrouchInput();

        ManageAnimations();

        if (_rb.velocity.magnitude >= _velocityClamp)
        {
            _rb.velocity = new Vector3(_velocityClamp, _rb.velocity.y, 0f);
        }

        playerSpeed?.Invoke(_rb.velocity.x);
    }

    // Fixed Update is used for the physics methods with the player's rigidbody
    private void FixedUpdate()
    {
        _rb.AddForce(Vector3.right * _forwardForce * Time.deltaTime);

        JumpPhysics();

        Crouch();
    }

    #region Input Methods
    void JumpInput()
    {
        // Checks player input for jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) && _isGrounded)
        {
            // Allows player to jump if the player is grounded and presses the up arrow key
            _canJump = true;
        } else if (Input.GetKeyUp(KeyCode.UpArrow) && !_isGrounded && _rb.velocity.y > 2)
        {
            // If the player let's go of the up arrow and the y velocity is still positive
            // Additional gravity force will be added to the player to land faster
            // This is to make the controls feel less floaty
            _rb.AddForce(Vector3.down * _addedGravity * Time.deltaTime, ForceMode.Impulse);
        }
    }

    void CrouchInput()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _isCrouching = false;
        }
    }
    #endregion

    void JumpPhysics()
    {
        if (_canJump)
        {
            //Player is launched with a jump force
            _rb.AddForce(Vector3.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
            _canJump = false;
            _isGrounded = false;
        }
    }

    void Crouch()
    {
        if (_isCrouching)
        {
            // Change the collider properties to the crouch properties
            _collider.center = new Vector3(_collider.center.x, _crouchColliderCenterY, _collider.center.z);
            _collider.height = _crouchColliderHeight;
        }
        else
        {
            // Revert the collider properties back to their original properties
            _collider.center = new Vector3(_collider.center.x, _originalColliderCenterY, _collider.center.z);
            _collider.height = _originalColliderHeight;
        }
    }

    void ManageAnimations()
    {
        _animator.SetBool("isGrounded", _isGrounded);
        _animator.SetBool("isCrouching", _isCrouching);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case ("Enemy"):
                // If the player collides with an enemy, invoke a unity action to end the level
                // Which will be picked up by the game manager
                playerDead?.Invoke();
                break;
            case ("Ground"):
                _isGrounded = true;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            playerWin?.Invoke();
        }
    }
}
