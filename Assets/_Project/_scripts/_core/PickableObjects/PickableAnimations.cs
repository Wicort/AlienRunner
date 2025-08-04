using DG.Tweening;
using UnityEngine;

public class PickableAnimations : MonoBehaviour
{
    public float rotationDuration = 2f;
    public Vector3 rotationAxis = Vector3.up;
    public int loops = -1;

    public float bobHeight = .5f;
    public float bobSpeed = .5f;
    public Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        transform.DORotate(rotationAxis * 360, rotationDuration, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetLoops(loops)
            .SetEase(Ease.Linear);

        
        transform.DOMoveY(initialPosition.y + bobHeight, 1f / (bobSpeed * 2)) // Полупериод
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine)
                 .SetRelative(false);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
