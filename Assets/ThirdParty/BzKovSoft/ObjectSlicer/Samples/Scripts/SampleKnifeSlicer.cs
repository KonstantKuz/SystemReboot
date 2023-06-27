using System.Collections;
using UnityEngine;

namespace BzKovSoft.ObjectSlicer.Samples
{
	/// <summary>
	/// Knife visual effect implementation
	/// </summary>
	public class SampleKnifeSlicer : MonoBehaviour
	{
#pragma warning disable 0649
		[SerializeField]
		private GameObject _blade;
#pragma warning restore 0649

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				var knife = _blade.GetComponentInChildren<BzKnife>();
				knife.BeginNewSlice();
			}
		}
	}
}