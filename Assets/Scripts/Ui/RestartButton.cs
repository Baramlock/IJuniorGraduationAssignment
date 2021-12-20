using UnityEngine;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private GameObject _button;

    private void OnEnable()
    {
        Player.Died += ViewRestartPlace;
    }

    private void OnDisable()
    {
        Player.Died += ViewRestartPlace;
    }

    private void ViewRestartPlace()
    {
        Time.timeScale = 0;
        _button.SetActive(true);
    }
}