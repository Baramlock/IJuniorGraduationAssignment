using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private GameObject _button;

    public void OnButtonClic()
    {
        _spawner.Restart();
         Time.timeScale = 1;
        _button.SetActive(false);
    }
}
