using System.Collections.Generic;
using UnityEngine;

namespace UnityFPS.Scripts
{
    public class PrefabReplacer : MonoBehaviour
    {
        [System.Serializable]
        public struct ReplacementDefinition
        {
            public GameObject SourcePrefab;
            public GameObject TargetPrefab;
        }

        public bool switchOrder;
        public List<ReplacementDefinition> replacements = new List<ReplacementDefinition>();
    }
}