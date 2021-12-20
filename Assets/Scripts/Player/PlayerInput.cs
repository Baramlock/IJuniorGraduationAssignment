using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float VerticalDirection { get; private set; }
    public float HorizontalDirection { get; private set; }

    private void Update()
    {
        VerticalDirection = Input.GetAxisRaw("Vertical");
        HorizontalDirection = Input.GetAxisRaw("Horizontal");
    }
}