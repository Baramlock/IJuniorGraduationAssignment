public class PlayerMoveTransit : Transition
{
    private void OnEnable()
    {
        PlayerMovement.PlayerHalfMoved += Transit;

    }
    private void OnDisable()
    {
        NeedTransit = false;
        PlayerMovement.PlayerHalfMoved -= Transit;
    }

    public void Transit()
    {
        NeedTransit = true;
    }
}
