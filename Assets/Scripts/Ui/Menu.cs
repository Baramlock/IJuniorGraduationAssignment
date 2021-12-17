using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _Panel;
    private void Start()
    {
        Time.timeScale = 0;
        _Panel.SetActive(true);
    }

    public void OpenPanel()
    {
        _Panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ClosePanel()
    {
        _Panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
