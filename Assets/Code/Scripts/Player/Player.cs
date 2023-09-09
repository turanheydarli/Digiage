using Code.Scripts.Roads;
using DG.Tweening;
using UnityEngine;

namespace Code.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Road road;
        [SerializeField] private DOTweenPath path;

        public DOTweenPath Path => path;

        public void OnReachEnd(float percent)
        {
            road = road.NextRoad;

            path.wps = road.Path.wps;
            path.DOPlay();
        }
    }
}