using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

    
public class Player : MonoBehaviour
{

    [Header("Movement")]
    public float speed;
    public float jumpSpeed;
    public float movementXLerp;
    private bool grounded, groundedLastFrame;
    private bool canJump;
    private Vector2 input;

    [Header("Physics")]
    public Rigidbody2D rb2D;
    private Vector2 velocity;
    private ContactPoint2D[] contacts = new ContactPoint2D[8];

    [Header("Camera")]
    public float screenShakeAmount;
    public float screenShakeDecay;
    public float screenShakeMax;
    public CinemachineVirtualCamera virtualCam;
    private CinemachineBasicMultiChannelPerlin noise;

    [Header("Animation")]
    public Animator spriteAnimator;
    public Vector2 runningSpeedRange;
    public Transform spriteTransform;

    // [Header("Miscellaneous")]
    

    void Awake()
    {
        groundedLastFrame = true; // triggers Midair anim correctly
        noise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        if (GameManager.Instance)
        {
            GameManager.Instance.player = this;
        }
        else
        {
            Debug.LogError("No GameManager in this scene! One must be added!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(0);
        }

        // Gather inputs
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        bool jumpInput = Input.GetButtonDown("Jump");

        // Grounded flagging before anything else
        grounded = rb2D.GetContacts(contacts) > 0;

        #region Movement + Jump

        // Horizontal movement
        velocity = new Vector2(input.x * speed, rb2D.velocity.y);

        // Jump (uses canJump instead of grounded)
        if (jumpInput && canJump)
        {
            canJump = false;
            velocity = new Vector2(velocity.x, jumpSpeed);
            // Force RB velocity, issues arise otherwise
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        }

        #endregion

        #region Animation

        // Set parameters
        spriteAnimator.SetBool("Grounded", grounded);
        spriteAnimator.SetInteger("MovingDir", (int) input.x);
        spriteAnimator.SetFloat("MovementSpeed", Mathf.Abs(rb2D.velocity.x));

        // Set facing direction
        if (input.x != 0)
        {
            spriteTransform.localScale = new Vector3(input.x, 1, 1);
        }

        // Set midair anim
        if (groundedLastFrame && !grounded)
        {
            spriteAnimator.Play("Midair", 0, 0);
        }

        // Set running speed
        if (spriteAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Running")
        {
            float runAnimSpd = Util.InverseLerpUnclamped(
                runningSpeedRange.x, runningSpeedRange.y, Mathf.Abs(rb2D.velocity.x));
            spriteAnimator.speed = runAnimSpd;
        }
        else
        {
            spriteAnimator.speed = 1;
        }

        #endregion

        groundedLastFrame = grounded;
    }

    void LateUpdate()
    {
        // SCREEN SHAKE
        screenShakeAmount = Mathf.Clamp(screenShakeAmount - screenShakeDecay * Time.deltaTime, 0, screenShakeMax);
        noise.m_AmplitudeGain = screenShakeAmount;
    }

    void FixedUpdate()
    {
        // Apply velocity to RB2D, incl. horizontal smoothing
        rb2D.velocity = new Vector2(Mathf.Lerp(rb2D.velocity.x, velocity.x, movementXLerp), velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        #region canJump Flagging

        int contactCount = col.GetContacts(contacts);

        for (int i = 0; i < contactCount; ++i)
        {
            Vector2 norm = contacts[i].normal;

            // Reject empty normals
            if (norm == Vector2.zero) 
                continue;

            float colAngle = Mathf.Atan2(norm.y, norm.x) * Mathf.Rad2Deg;

            // Assuming gravity is always "down", valid ground angles are between 45 degree slopes
            if (Mathf.Abs(colAngle - 90) <= 45)
            {
                canJump = true;
                // Did the job, escape early
                break;
            }
        }

        #endregion
    }
}
