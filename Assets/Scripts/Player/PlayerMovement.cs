using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _blockMovePointOneX;
    [SerializeField] private float _blockMovePointOneZ;
    [SerializeField] private float _blockMovePointTwoX;
    [SerializeField] private float _blockMovePointTwoZ;

    public static event UnityAction StartMove;
    public static event UnityAction HalfMoved;
    public static event UnityAction EndMove;

    private float _scale;
    private bool _isRolling;

    private void Start()
    {
        _scale = transform.localScale.x / 2;
    }

    public void TryMove(Vector3 direction)
    {
        if (_isRolling == false)
        {
            if (transform.position.x <= _blockMovePointOneX && direction.x < 0 ||
                transform.position.x >= _blockMovePointTwoX && direction.x > 0)
            {
                return;
            }

            if (transform.position.z <= _blockMovePointOneZ && direction.z < 0 ||
                transform.position.z >= _blockMovePointTwoZ && direction.z > 0)
            {
                return;
            }

            _isRolling = true;
            StartMove?.Invoke();
            StartCoroutine(RollingCube(direction));
        }
    }

    private IEnumerator RollingCube(Vector3 direction)
    {
        var elapsed = 0f;
        var isHalfTimeDuration = true;
        FindValueByRotate(direction, out Vector3 transformPoint, out Vector3 axis, out float angle,
            out Vector3 newPosition);

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            if (elapsed > _duration / 2 && isHalfTimeDuration)
            {
                HalfMoved?.Invoke();
                isHalfTimeDuration = false;
            }

            transform.RotateAround(transformPoint, axis, angle / _duration * Time.deltaTime);
            yield return null;
        }

        transform.position = newPosition;
        transform.rotation = Quaternion.identity;
        _isRolling = false;
        EndMove?.Invoke();
    }

    private void FindValueByRotate(Vector3 direction, out Vector3 transformPoint, out Vector3 axis, out float angle,
        out Vector3 newPosition)
    {
        var position = transform.position;
        if (direction.z != 0)
        {
            axis = Vector3.left;
            angle = direction.z < 0 ? 90 : -90;
        }
        else if (direction.x != 0)
        {
            axis = Vector3.forward;
            angle = direction.x < 0 ? 90 : -90;
        }
        else
        {
            axis = Vector3.left;
            angle = direction.y < 0 ? 90 : -90;
        }

        transformPoint = position + (direction + Vector3.down) * _scale;
        newPosition = position + direction;
    }
}