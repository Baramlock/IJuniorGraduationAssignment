using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Place _startPlace;
    [SerializeField] private List<Place> _assembledPlaces;
    [SerializeField] private GameObject _player;
    [SerializeField] private FollowTarget _camera;
    [SerializeField] private Vector3 _playerStartPosition;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private int _duration = 4;
    [SerializeField] private int _startLeanght = 15;

    [SerializeField] private int _maxPlace;
    private int _windth = 3;
    private List<Place> _places;

    private void Awake()
    {
        _places = new List<Place>();
        for (int i = 0; i < _startLeanght; i++)
        {
            for (int j = 0; j < _windth; j++)
            {
                var place = Instantiate(_startPlace, _startPosition + new Vector3(j, 0, i), Quaternion.identity);
            }
        }
        var player = Instantiate(_player, _playerStartPosition, Quaternion.identity);
        _camera.Init(player);
    }

    private void OnEnable()
    {
        Player.ScoreChanged += BuildPlace;
    }

    private void OnDisable()
    {
        Player.ScoreChanged -= BuildPlace;
    }

    private void BuildPlace(int distans)
    {
        if (_places.Count < _maxPlace)
        {
            var newPlace = Instantiate(_assembledPlaces[Random.Range(0, _assembledPlaces.Count)], _startPosition + new Vector3(0, 7, _startLeanght + distans - 1), Quaternion.identity);
            newPlace.transform.DOMove(_startPosition + new Vector3(0, 0, _startLeanght + distans - 1), _duration);
            _places.Add(newPlace);
        }
        else
        {
            int placeIndex = Random.Range(0, (int)0.5 * _maxPlace);
            _places[placeIndex].gameObject.SetActive(true);
            _places[placeIndex].transform.position = _startPosition + new Vector3(0, 7, _startLeanght + distans - 1);
            _places[placeIndex].transform.DOMove(_startPosition + new Vector3(0, 0, _startLeanght + distans - 1), _duration);
            _places.Add(_places[placeIndex]);
            _places.RemoveAt(placeIndex);
        }
    }

    public void Restart()
    {
        var player = Instantiate(_player, _playerStartPosition, Quaternion.identity);
        _camera.Init(player);
        foreach (var place in _places)
        {
            place.gameObject.SetActive(false);
        }
    }
}
