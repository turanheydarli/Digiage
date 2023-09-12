using System;
using Code.Scripts.ColorManipulators;
using Code.Scripts.Models;
using Code.Scripts.Players;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Code.Scripts.Roads
{
    public class RoadPiece : MonoBehaviour
    {
        private float _obstacleDistance;
        private float _obstaclePercentage;

        private Player _player;
        private ColorModel _currentColor;
        private Material _defaultColor;
        private bool _isObstacle;
        private bool _isPastObstacle;
        private bool _isInitialized;
        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        public void Initialize(float obstacleDistance, float obstaclePercentage)
        {
            _obstacleDistance = obstacleDistance;
            _obstaclePercentage = obstaclePercentage;

            _isInitialized = true;
        }

        private void Start()
        {
            _player = FindObjectOfType<Player>();

            _defaultColor = _renderer.material;

            SkyManipulator.OnSkyColorChange += ChangeColor;
            SkyManipulator.OnSkyColorChange += SetAsObstacle;
        }

        private void SetAsObstacle(ColorModel colorModel)
        {
            if(!_isInitialized)
                return;

            _currentColor = colorModel;

            if (Vector3.Distance(_player.transform.position, transform.position) > _obstacleDistance)
            {
                if (_obstaclePercentage < Random.value)
                {
                    _isObstacle = true;
                    gameObject.SetActive(false);
                }
            }
        }

        private void ChangeColor(ColorModel colorModel)
        {
            if(!_isInitialized)
                return;

            _isPastObstacle = false;

            if (_isObstacle)
            {
                _isObstacle = false;
                _isPastObstacle = true;

                gameObject.SetActive(true);

                if (Vector3.Distance(_player.transform.position, transform.position) > _obstacleDistance)
                {
                    _renderer.material.color = _defaultColor.color;
                }
                else
                {
                    _renderer.material.color = _currentColor.bottomColor;
                }
            }
        }
    }
}