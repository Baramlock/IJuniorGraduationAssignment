using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    public static event UnityAction<int> ScoreChanged;
    public static event UnityAction Die;

    private int _score;
    private PlayerInput _playerInput;
    private PlayerMovement _playerMove;
    private float _distans;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMove = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_playerInput.VerticalDirection != 0)
        {
            _playerMove.TryMove(new Vector3(0, 0, _playerInput.VerticalDirection));
            _distans = transform.position.z;
            CountScore();
        }
        else if (_playerInput.HorizontalDirection != 0)
        {
            _playerMove.TryMove(new Vector3(_playerInput.HorizontalDirection, 0, 0));
        }

    }

    public void Dies()
    {
        Destroy(gameObject);
        Die?.Invoke();
    }

    private void CountScore()
    {
        if (_score < (int)_distans)
        {
            _score = (int)_distans;
            ScoreChanged?.Invoke(_score);
        }
    }

}
