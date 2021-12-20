using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Renderer))]
public class MagmaState : State
{
    private BoxCollider _boxCollider;
    private Renderer _renderer;
    private Color _startColor;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _boxCollider.enabled = true;
        _startColor = _renderer.material.color;
        _renderer.material.color = Color.red;
    }

    private void OnDisable()
    {
        _boxCollider.enabled = false;
        _renderer.material.color = _startColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            player.Die();
        }
    }
}