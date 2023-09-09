using System.Collections.Generic;
using Core.Utilities.Singletons;
using UnityEngine;

namespace Code.Scripts.Roads
{
    public class RoadHolder : SingletonBase<RoadHolder>
    {
        [SerializeField] private List<Road> roads;

        public Road GetRandomRoad()
        {
            return roads[0];
        }
    }
}