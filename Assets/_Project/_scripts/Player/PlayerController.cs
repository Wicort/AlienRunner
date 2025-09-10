using Assets._Project._scripts;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float laneOffset = 2f;
    public float laneChangeSpeed = 10f;
    public float jumpPower = 15f;
    public float jumpGravty = -40f;
    public Animator animator;
    

    private Rigidbody rb;
    private float pointStart;
    private float pointFinish;
    private bool isMoving;
    private Coroutine movingCoroutine;
    private float lastVectorX;
    private Vector3 startGamePosition;
    private Quaternion startGameRotation;
    private bool isJumping;
    private float realGravity = -9.8f;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startGamePosition = transform.position;
        startGameRotation = transform.rotation;

        SwipeManager.Instance.MoveEvent += MovePlayer;
    }

    private void OnDestroy()
    {
        SwipeManager.Instance.MoveEvent -= MovePlayer;
    }

    private void MovePlayer(bool[] swipes)
    {
        if (swipes[(int)SwipeManager.Direction.Left] && pointFinish > -laneOffset)
        {
            MoveHorizontal(-laneChangeSpeed);
        }

        if (swipes[(int)SwipeManager.Direction.Right] && pointFinish < laneOffset)
        {
            MoveHorizontal(laneChangeSpeed);
        }

        if (swipes[(int)SwipeManager.Direction.Up] && !isJumping)
        {
            Jump();
        }
    }

    private void Jump()
    {
        isJumping = true;
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        Physics.gravity = new Vector3(0, jumpGravty, 0);
        animator.SetTrigger("Jump");
        StartCoroutine(StopJumpingCoroutine());
    }

    private IEnumerator StopJumpingCoroutine()
    {
        do
        {
            yield return new WaitForFixedUpdate();
        } while (rb.linearVelocity.y != 0);

        isJumping = false;
        Physics.gravity = new Vector3(0, realGravity, 0);
    }

    private void MoveHorizontal(float speed)
    {
        pointStart = pointFinish;
        pointFinish += Mathf.Sign(speed) * laneOffset;

        if (isMoving) 
        { 
            StopCoroutine(movingCoroutine); 
            isMoving = false;
        }
        movingCoroutine = StartCoroutine(MoveCoroutine(speed));
    }

    IEnumerator MoveCoroutine (float vectorX)
    {
        isMoving = true;
        //animator.SetTrigger(Mathf.Sign(vectorX) < 0 ? "RightStrafe" : "LeftStrafe");
        while (Mathf.Abs(pointStart - transform.position.x) < laneOffset)
        {
            yield return new WaitForFixedUpdate();

            rb.linearVelocity = new Vector3(vectorX, rb.linearVelocity.y, 0);
            lastVectorX = vectorX;
            float x = Mathf.Clamp(transform.position.x, Mathf.Min(pointStart, pointFinish), Mathf.Max(pointStart, pointFinish));
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        rb.linearVelocity = Vector3.zero;
        transform.position = new Vector3(pointFinish, transform.position.y, transform.position.z);

        if (transform.position.y > 1)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -10, rb.linearVelocity.z);
        }

        isMoving = false;
    }

    public void StartGame()
    {
        RoadGenerator.Instance.StartLevel();
        CameraSwitcher.Instance.SwitchTo(CameraSwitcher.CameraMode.GameplayCamera);
        animator.SetBool("IsStarted", true);
    }


    public void ResetGame()
    {
        rb.linearVelocity = Vector3.zero;
        pointStart = 0;
        pointFinish = 0;
        transform.position = startGamePosition;
        transform.rotation = startGameRotation;
        RoadGenerator.Instance.ResetLevel();
        animator.SetBool("IsStarted", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ramp")
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionZ;
        }

        if (other.gameObject.tag == "Lose")
        {
            ResetGame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ramp")
        {
            animator.SetTrigger("Grounding");
            rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            animator.SetTrigger("Grounding");
        }

        if (collision.gameObject.tag == "NotLose")
        {
            MoveHorizontal(-lastVectorX);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "RampPlane")
        {
            if (rb.linearVelocity.x == 0 && !isJumping)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, -10, rb.linearVelocity.z);
            }
        }
    }

}
