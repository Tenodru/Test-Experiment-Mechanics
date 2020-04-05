using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] GameObject mainCam;
    [SerializeField] LayerMask platformLayerMask;

    [Header("Player Stats")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 5f;

    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);    //Creates new Vector3 with x-coordinate value between -1 and 1.

        transform.position += movement * Time.deltaTime * moveSpeed;            //Moves player character.
        mainCam.transform.position += movement * Time.deltaTime * moveSpeed;    //Moves camera.
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && CanJump())
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);  //Applies an instant vertical impulse force.
            Debug.Log("Player jumped.");
        }
    }

    /// <summary>
    /// Checks if player is/isn't in the air, if player is in the air then player cannot jump.
    /// </summary>
    /// <returns></returns>
    bool CanJump()
    {
        float heightError = 0.05f;
        RaycastHit2D hit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.down, boxCollider2D.bounds.extents.y + heightError, platformLayerMask);
        Color rayColor;

        if (hit.collider != null)
            rayColor = Color.green;
        else
            rayColor = Color.red;

        Debug.DrawRay(boxCollider2D.bounds.center, Vector2.down * (boxCollider2D.bounds.extents.y + heightError), rayColor);
        Debug.Log(hit.collider);
        Debug.Log(hit.collider != null);
        return hit.collider != null;
    }
}
