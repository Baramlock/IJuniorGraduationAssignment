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

    public static event UnityAction PlayerStartMove;
    public static event UnityAction PlayerHalfMoved;
    public static event UnityAction PlayerFullMove;

    private Transform _cube;
    private float _scale;
    private bool _isRolling;

    public float Duration => _duration;

    private void Start()
    {
        _cube = GetComponent<Transform>();
        _scale = _cube.localScale.x / 2;
    }

    public void TryMove(Vector3 direction)
    {
        if (_isRolling == false)
        {
            if (_cube.transform.position.x <= _blockMovePointOneX && direction.x < 0 || _cube.transform.position.x >= _blockMovePointTwoX && direction.x > 0)
            {
                return;
            }
            else if (_cube.transform.position.z <= _blockMovePointOneZ && direction.z < 0 || _cube.transform.position.z >= _blockMovePointTwoZ && direction.z > 0)
            {
                return;
            }
            _isRolling = true;
            PlayerStartMove?.Invoke();
            StartCoroutine(RollingCube(direction));
        }
    }

    private IEnumerator RollingCube(Vector3 direction)
    {
        float elapset = 0f;
        bool isHaflTimeDuration = true;
        ReturnValueByRotate(direction, out Vector3 transformPoint, out Vector3 axis, out float angle, out Vector3 _newPosition);

        while (elapset < _duration)
        {
            elapset += Time.deltaTime;
            if (elapset > _duration / 2 && isHaflTimeDuration)
            {
                PlayerHalfMoved?.Invoke();
                isHaflTimeDuration = false;
            }

            _cube.RotateAround(
                transformPoint,
                axis,
                angle / _duration * Time.deltaTime);

            yield return null;
        }

        _cube.position = _newPosition;
        _cube.rotation = Quaternion.identity;
        _isRolling = false;
        PlayerFullMove?.Invoke();
    }

    private void ReturnValueByRotate(Vector3 direction, out Vector3 transformPoint, out Vector3 axis, out float angle, out Vector3 newPosition)
    {
        if (direction.z != 0)
        {
            axis = Vector3.left;
            transformPoint = _cube.position + (Vector3.forward * direction.z + Vector3.down) * _scale;
            newPosition = _cube.position + Vector3.forward * direction.z;
            angle = direction.z < 0 ? 90 : -90;
        }
        else if (direction.x != 0)
        {
            axis = Vector3.forward;
            transformPoint = _cube.position + (Vector3.right * direction.x + Vector3.down) * _scale;
            newPosition = _cube.position + Vector3.right * direction.x;
            angle = direction.x < 0 ? 90 : -90;
        }
        else
        {
            axis = Vector3.left;
            transformPoint = _cube.position + (Vector3.forward + Vector3.up * direction.y) * _scale;
            newPosition = _cube.position + Vector3.up * direction.y;
            angle = direction.y < 0 ? 90 : -90;
        }
    }
}
