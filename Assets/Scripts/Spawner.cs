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
    [SerializeField] private int _descentHeight = 7;
    [SerializeField] private int _maxPlace;
     

    private int _windth = 3;
    private List<Place> _placesActive;
    private Pool<Place> _prefabsPool;

    private void Awake()
    {
        _placesActive = new List<Place>();

        for (int i = 0; i < _startLeanght; i++)
        {
            for (int j = 0; j < _windth; j++)
            {
                var place = Instantiate(_startPlace, _startPosition + new Vector3(j, 0, i), Quaternion.identity);
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

    private void BuildPlace(int distans)
    {
        var startPosition = _startPosition + new Vector3(0, _descentHeight, _startLeanght + distans - 1);
        var position = startPosition + Vector3.down * _descentHeight;

        if (_placesActive.Count >= _maxPlace)
        {
            for (int i = 0; i < _maxPlace / 2; i++)
            {
                _prefabsPool.ReturnInPool(_placesActive[0]);
                _placesActive.RemoveAt(0);
            }
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
