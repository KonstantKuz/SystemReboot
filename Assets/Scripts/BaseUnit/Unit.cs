using Common;
using Extension;
using UnityEngine;

namespace BaseUnit
{
    public class Unit : MonoBehaviour, IObjectRoot
    {
        private ITarget _selfTarget;
        public GameObject Root => gameObject;
        public ITarget SelfTarget => _selfTarget ??= gameObject.RequireComponent<ITarget>();
    }
}