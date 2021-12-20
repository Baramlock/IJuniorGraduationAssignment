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
    [SerializeField] private int _startLength = 15;
    [SerializeField] private int _descentHeight = 7;
    [SerializeField] private int _maxPlace;

    private int _width = 3;
    private List<Place> _placesActive;
    private Pool<Place> _prefabsPool;

    private void Awake()
    {
        _placesActive = new List<Place>();

        for (int i = 0; i < _startLength; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                Instantiate(_startPlace, _startPosition + new Vector3(j, 0, i), Quaternion.identity);
            }
        }

        var player = Instantiate(_player, _playerStartPosition, Quaternion.identity);
        _camera.Init(player);

        _prefabsPool = new Pool<Place>();
        _prefabsPool.AddPoolPrefabs(_assembledPlaces);
    }

    private void OnEnable()
    {
        Player.ScoreChanged += BuildPlace;
    }

    private void OnDisable()
    {
        Player.ScoreChanged -= BuildPlace;
    }

    private void BuildPlace(int distance)
    {
        var startPosition = _startPosition + new Vector3(0, _descentHeight, _startLength + distance - 1);
        var position = startPosition + Vector3.down * _descentHeight;

        if (_placesActive.Count >= _maxPlace)
        {
            _prefabsPool.ReturnInPool(_placesActive.GetRange(0, _maxPlace / 2));
            _placesActive.RemoveRange(0, _maxPlace / 2);
        }

        var newPlace = _prefabsPool.GetFromPool();
        newPlace.transform.position = startPosition;
        newPlace.transform.DOMove(position, _duration);
        _placesActive.Add(newPlace);
    }

    public void Restart()
    {
        var player = Instantiate(_player, _playerStartPosition, Quaternion.identity);
        _camera.Init(player);
        foreach (var place in _placesActive)
            _prefabsPool.ReturnInPool(place);
        _placesActive.Clear();
    }
}