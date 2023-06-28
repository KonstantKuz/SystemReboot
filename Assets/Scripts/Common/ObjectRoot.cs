using UnityEngine;

namespace Common
{
    public class ObjectRoot : MonoBehaviour, IObjectRoot
    {
        public GameObject Root => gameObject;
    }
}