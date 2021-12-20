using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;

    private void Start()
    {
        Time.timeScale = 0;
        _panel.SetActive(true);
    }

    public void OpenPanel()
    {
        _panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePanel()
    {
        _panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}