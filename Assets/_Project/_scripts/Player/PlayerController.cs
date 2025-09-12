using Assets._Project._scripts;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _laneOffset = 2f;
    [SerializeField] private float _laneChangeSpeed = 10f;
    [SerializeField] private float _jumpPower = 15f;
    [SerializeField] private float _jumpGravty = -40f;
    [SerializeField] private Animator _animator;
    [SerializeField] private CapsuleCollider _collider;
    
    private Rigidbody _rb;
    private float _pointStart;
    private float _pointFinish;
    private bool _isMoving;
    private Coroutine _movingCoroutine;
    private float _lastVectorX;
    private Vector3 _startGamePosition;
    private Quaternion _startGameRotation;
    private bool _isJumping;
    private bool _isSliding;
    private float _realGravity = -9.8f;

    private int IsStartedHash = Animator.StringToHash("IsStarted");
    private int GroundingHash = Animator.StringToHash("Grounding");
    private int JumpHash = Animator.StringToHash("Jump");
    private int SlideHash = Animator.StringToHash("Slide");


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startGamePosition = transform.position;
        _startGameRotation = transform.rotation;

        SwipeManager.Instance.MoveEvent += MovePlayer;
    }

    private void OnDestroy()
    {
        SwipeManager.Instance.MoveEvent -= MovePlayer;
    }

    private void MovePlayer(bool[] swipes)
    {
        if (swipes[(int)SwipeManager.Direction.Left] && _pointFinish > -_laneOffset)
        {
            MoveHorizontal(-_laneChangeSpeed);
        }

        if (swipes[(int)SwipeManager.Direction.Right] && _pointFinish < _laneOffset)
        {
            MoveHorizontal(_laneChangeSpeed);
        }

        if (swipes[(int)SwipeManager.Direction.Up] && !_isJumping && !_isSliding)
        {
            Jump();
        }

        if (swipes[(int)SwipeManager.Direction.Down] /*&& !_isJumping*/ && !_isSliding)
        {
            Slide();
        }
    }

    private void Slide()
    {
        _isSliding = true;
        _animator.SetTrigger(SlideHash);
        _collider.height = 1;
        _collider.center = new Vector3(0, 0.5f,0);
        StartCoroutine(StopSlidingCoroutine());
    }

    private IEnumerator StopSlidingCoroutine()
    {
        yield return new WaitForSeconds(.7f);
        _isSliding = false;
        _collider.height = 2;
        _collider.center = new Vector3(0, 1f, 0);
    }

    private void Jump()
    {
        _isJumping = true;
        _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        Physics.gravity = new Vector3(0, _jumpGravty, 0);
        _animator.SetTrigger(JumpHash);
        StartCoroutine(StopJumpingCoroutine());
    }

    private IEnumerator StopJumpingCoroutine()
    {
        do
        {
            yield return new WaitForFixedUpdate();
        } while (_rb.linearVelocity.y != 0);

        _isJumping = false;
        Physics.gravity = new Vector3(0, _realGravity, 0);
    }

    private void MoveHorizontal(float speed)
    {
        _pointStart = _pointFinish;
        _pointFinish += Mathf.Sign(speed) * _laneOffset;

        if (_isMoving) 
        { 
            StopCoroutine(_movingCoroutine); 
            _isMoving = false;
        }
        _movingCoroutine = StartCoroutine(MoveCoroutine(speed));
    }

    IEnumerator MoveCoroutine (float vectorX)
    {
        _isMoving = true;
        while (Mathf.Abs(_pointStart - transform.position.x) < _laneOffset)
        {
            yield return new WaitForFixedUpdate();

            _rb.linearVelocity = new Vector3(vectorX, _rb.linearVelocity.y, 0);
            _lastVectorX = vectorX;
            float x = Mathf.Clamp(transform.position.x, Mathf.Min(_pointStart, _pointFinish), Mathf.Max(_pointStart, _pointFinish));
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        _rb.linearVelocity = Vector3.zero;
        transform.position = new Vector3(_pointFinish, transform.position.y, transform.position.z);

        if (transform.position.y > 1)
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, -10, _rb.linearVelocity.z);
        }

        _isMoving = false;
    }

    public void StartGame()
    {
        RoadGenerator.Instance.StartLevel();
        CameraSwitcher.Instance.SwitchTo(CameraSwitcher.CameraMode.GameplayCamera);
        _animator.SetBool(IsStartedHash, true);
    }

    public void ResetGame()
    {
        _rb.linearVelocity = Vector3.zero;
        _pointStart = 0;
        _pointFinish = 0;
        transform.position = _startGamePosition;
        transform.rotation = _startGameRotation;
        RoadGenerator.Instance.ResetLevel();
        _animator.SetBool(IsStartedHash, false);
        CameraSwitcher.Instance.SwitchTo(CameraSwitcher.CameraMode.MenuCamera);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ramp")
        {
            _rb.constraints |= RigidbodyConstraints.FreezePositionZ;
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
            _animator.SetTrigger(GroundingHash);
            _rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0, _rb.linearVelocity.z);
            _animator.SetTrigger(GroundingHash);
        }

        if (collision.gameObject.tag == "NotLose")
        {
            MoveHorizontal(-_lastVectorX);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "RampPlane")
        {
            if (_rb.linearVelocity.x == 0 && !_isJumping)
            {
                _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, -10, _rb.linearVelocity.z);
            }
        }
    }

}
