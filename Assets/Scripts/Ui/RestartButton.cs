using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private GameObject _button;
    private void OnEnable()
    {
        Player.Die += ViewRestartPlace;
    }

    private void OnDisable()
    {
        Player.Die += ViewRestartPlace;
    }

    private void ViewRestartPlace()
    {
        Time.timeScale = 0;
        _button.SetActive(true);
    }
}
