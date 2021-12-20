public class PlayerMoveTransit : Transition
{
    private void OnEnable()
    {
        PlayerMovement.HalfMoved += Transit;
    }

    private void OnDisable()
    {
        NeedTransit = false;
        PlayerMovement.HalfMoved -= Transit;
    }

    private void Transit()
    {
        NeedTransit = true;
    }
}