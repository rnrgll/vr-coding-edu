using System;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

namespace Managers
{
	public class UIManager : Singleton<UIManager>
	{	
		[SerializeField] private GameObject addValueUI;
		private void Awake()
		{
			SingletonInit();
		}

	
		
		
		public void OpenAddValueUI()
		{
			addValueUI.SetActive(true);
		}

	}
}