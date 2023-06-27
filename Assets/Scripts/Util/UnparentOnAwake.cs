using System;
using UnityEngine;

namespace Util
{
    public class UnparentOnAwake : MonoBehaviour
    {
        private void Awake()
        {
            transform.SetParent(transform.parent.parent);
        }
    }
}
