using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Scripts.ColorManipulators;
using Code.Scripts.Models;
using Dreamteck.Splines;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace Code.Scripts.Roads
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private float obstacleDistance;
        [SerializeField] private float obstaclePercentage;

        [SerializeField] private List<RoadPiece> pieces;

        [SerializeField] private SplineComputer computer;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        [SerializeField] private MeshRenderer defaultMaterial;
        public SplineComputer Computer => computer;
        public Road NextRoad => _nextRoad;

        private Road _nextRoad;

        private void Awake()
        {
            foreach (var piece in pieces)
            {
                piece.Initialize(obstacleDistance, obstaclePercentage);
            }
        }

        private void Start()
        {
            //  StartCoroutine(GenerateRoad());
        }

        private void ResetOtherColors(List<MeshRenderer> renderers)
        {
            foreach (var road in renderers)
            {
                road.material.SetColor("_Color", defaultMaterial.material.color);
            }
        }

        IEnumerator GenerateRoad()
        {
            yield return new WaitForSeconds(4f);

            Road road = RoadHolder.Instance.GetRandomRoad();

            _nextRoad = Instantiate(road, endPoint.position, Quaternion.identity);
        }
    }
}