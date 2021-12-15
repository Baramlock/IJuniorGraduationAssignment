using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Vector3 _startPosition;
    private Vector3 _startTargetPosition;
    private GameObject _target;

    private void Start()
    {
        _startPosition = transform.position;
        _startTargetPosition = _target.transform.position ;
    }

    private void Update()
    {
        if (_target != null)
        {
            transform.position = _target.transform.position + _startPosition - _startTargetPosition;
        }
    }

    public void Init(GameObject target)
    {
        _target = target;
    }
}
