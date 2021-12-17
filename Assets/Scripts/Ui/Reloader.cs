using UnityEngine;

public class Reloader : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _button;

    public void OnButtonClick()
    {
        _spawner.Restart();
        Time.timeScale = 1;
        _button.SetActive(false);
    }
}
