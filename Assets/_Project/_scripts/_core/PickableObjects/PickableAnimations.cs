using DG.Tweening;
using UnityEngine;

public class PickableAnimations : MonoBehaviour
{
    [Header("Вращение")]
    public float RotationDuration = 2f;
    public Vector3 RotationAxis = Vector3.up;

    [Header("Колебание вверх-вниз")]
    public float BobHeight = .5f;
    public float BobSpeed = .5f;
    
    private Vector3 _initialPosition;
    private int _loops = -1;

    private void Start()
    {
        _initialPosition = transform.position;
        transform.DORotate(RotationAxis * 360, RotationDuration, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetLoops(_loops)
            .SetEase(Ease.Linear)
            .SetId(transform);

        float moveDuration = 1f / (BobSpeed * 2f);
        transform.DOMoveY(_initialPosition.y + BobHeight, moveDuration)
                 .SetLoops(_loops, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine)
                 .SetRelative(false)
                 .SetId(transform);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
