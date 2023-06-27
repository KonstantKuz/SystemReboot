﻿using UnityEngine;
using System.Collections;

namespace NobleMuffins.LimbHacker.Examples
{
	[RequireComponent(typeof(Button))]
	public class RestartButton : MonoBehaviour
	{
		public Spawner spawner;

		private Button button;

		void Start()
		{
			button = gameObject.GetComponent<Button>();
		}

		void Update()
		{
			button.visible = spawner.CanInstantiate;
		}

		void OnClick()
		{
			spawner.Instantiate();
		}
	}
}
