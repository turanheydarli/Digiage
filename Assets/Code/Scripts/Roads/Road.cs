using System;
using System.Collections;
using DG.Tweening;
using Dreamteck.Splines;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Scripts.Roads
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private DOTweenPath path;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        public DOTweenPath Path => path;
        public Road NextRoad => _nextRoad;

        private Road _nextRoad;

        private void Start()
        {
            StartCoroutine(GenerateRoad());
        }

        IEnumerator GenerateRoad()
        {
            yield return new WaitForSeconds(0.1f);

            Road road = RoadHolder.Instance.GetRandomRoad();

            _nextRoad = Instantiate(road, endPoint.position, quaternion.identity);
        }
    }
}