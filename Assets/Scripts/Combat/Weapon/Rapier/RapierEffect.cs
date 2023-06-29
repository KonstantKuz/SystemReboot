using System.Linq;
using DigitalRuby.ThunderAndLightning;
using UnityEngine;

namespace Combat.Weapon.Rapier
{
    public class RapierEffect : MonoBehaviour
    {
        [SerializeField] private LightningBoltPathScriptBase _lightning;
        [SerializeField] private GameObject[] _rig;

        private void Awake()
        {
            _lightning.LightningPath = _rig.ToList();
        }
    }
}
