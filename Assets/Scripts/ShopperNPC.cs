using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopperNPC : MonoBehaviour
{
    [Header("Animation")]
    public float rotateMax;
    public Vector2 jumpHeightRange;
    public float jumpSpeed;
    public float jumpSpeedRandOffset;
    public float screenShakeAmount;
    bool rotCosineFlag;
    float animRot;
    Vector2 animJumpSpot;

    [Header("Knockout")]
    public float knockoutRotMax;
    public float knockoutRotSpdMax;
    public float knockoutForceMax;
    public float knockoutGravity;

    [Header("Movement")]
    public float speed;

    [Header("Components")]
    public LevelManager levelManager;
    public Collider2D collider;
    public Rigidbody2D rb2d;
    public SpriteRenderer spriteRenderer;
    Transform spriteObj;

    void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();

        AssignNewJumpSpot();

        spriteObj = spriteRenderer.gameObject.transform;

        jumpSpeed += Random.Range(-jumpSpeedRandOffset, jumpSpeedRandOffset);
    }

    void Update()
    {
        if (jumpSpeed > 0)
        {
            float cos = Mathf.Cos(Time.timeSinceLevelLoad * jumpSpeed);
            bool triggered = false;

            if (cos >= 0 && !rotCosineFlag)
            {
                rotCosineFlag = true;
                triggered = true;
            }
            else if (cos < 0 && rotCosineFlag)
            {
                rotCosineFlag = false;
                triggered = true;
            }

            if (triggered)
            {
                AssignNewJumpSpot();
            }

            spriteObj.localPosition = Vector3.Lerp(Vector3.zero,
                animJumpSpot, Mathf.Abs(cos));
            spriteObj.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(0, animRot, Mathf.Abs(cos)/2f));
        }
        else
        {
            spriteObj.localPosition = Vector3.zero;
            spriteObj.localRotation = Quaternion.identity;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // When hit by player, pause the game one frame and knock out of the game
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            col.otherCollider.gameObject.layer = LayerMask.NameToLayer("ShopperKnockOut");
            collider.gameObject.layer = LayerMask.NameToLayer("ShopperKnockOut");

            levelManager.currentShoppers--;
            StartCoroutine(KnockOutCoroutine());
            // Cleans up for us after more than enough time has passed
            Destroy(gameObject, 8);
        }
    }

    IEnumerator KnockOutCoroutine()
    {
        spriteRenderer.sortingLayerName = "ShopperKnockOut";

        jumpSpeed = 0;

        rb2d.constraints = RigidbodyConstraints2D.None;
        rb2d.gravityScale = knockoutGravity;
        rb2d.velocity = Util.Vec2Rotate(Vector2.up, Random.Range(-knockoutRotMax, knockoutRotMax)) * knockoutForceMax;
        rb2d.angularVelocity = Random.Range(-knockoutRotSpdMax, knockoutRotSpdMax);

        GameManager.Instance.player.screenShakeAmount += screenShakeAmount;

        // Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(Time.deltaTime);
        Time.timeScale = 1;
    }

    private void AssignNewJumpSpot()
    {
        // animJumpSpot is assigned a Vec2 offset according to the following:
        //  1) Take Vector2.up, rotate it some random degrees left/right
        //  2) (Vector being normalized already) scale by some "jump height" range

        // The player will cos bounce along the path needed to reach the point

        animRot = animJumpSpot.x < 0 ?
            Random.Range(-rotateMax, 0) :
            Random.Range(0, rotateMax);

        animJumpSpot = Util.Vec2Rotate(Vector2.up, animRot)
            * Random.Range(jumpHeightRange.x, jumpHeightRange.y);
    }
}
