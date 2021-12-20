using UnityEngine;

public class TeleportState : State
{
    [SerializeField] private Vector2 _direction;
    private BoxCollider _boxCollider;
    private Renderer _renderer;
    private Color _startColor;
    private bool _isMove;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _boxCollider.enabled = true;
        _startColor = _renderer.material.color;
        _renderer.material.color = Color.blue;
        PlayerMovement.StartMove += SetMoveTrue;
        PlayerMovement.EndMove += SetMoveFalse;
    }

    private void OnDisable()
    {
        _boxCollider.enabled = false;
        _renderer.material.color = _startColor;
        PlayerMovement.StartMove -= SetMoveTrue;
        PlayerMovement.EndMove -= SetMoveFalse;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player) && _isMove == false)
        {
            player.transform.position += (Vector3) _direction;
        }
    }

    private void SetMoveTrue()
    {
        _isMove = true;
    }

    private void SetMoveFalse()
    {
        _isMove = false;
    }
}