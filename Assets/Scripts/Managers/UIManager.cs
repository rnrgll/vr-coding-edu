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
		//	//UI Stack
		//	private GameObject _rootUI;
		//	private Stack<BaseUI> _UIStack = new Stack<BaseUI>();
		//	private int _order = 50;

		//	//공통 UI
		//	public SharedUI sharedUI;
		//	[SerializeField] private SharedUI sharedUIPrefab;

		//	//ui 오브젝트 풀
		//	private ObjectPool _interactUIPool;
		//	[SerializeField] private InteractUI _interactUIPrefab;
		//	[SerializeField] private Transform _poolTransform;

		//	public GameObject RootUI
		//	{
		//		get
		//		{
		//			if (_rootUI == null)
		//			{
		//				_rootUI = GameObject.Find("UI_Root");
		//				if (_rootUI == null)
		//				{
		//					_rootUI = new GameObject("UI_Root");
		//					if (FindObjectOfType<EventSystem>() == null)
		//					{
		//						GameObject eventSystem = new GameObject("EventSystem",
		//							typeof(EventSystem),
		//							typeof(InputSystemUIInputModule)
		//						);
		//						eventSystem.transform.SetParent(_rootUI.transform);
		//					}

		//				}


		//			}

		//			return _rootUI;
		//		}
		//	}

		//	// UI 활성화 여부
		//	public ObservableProperty<bool> IsUIActive= new ();


		//	//-----
		//	private void Awake() => Init();

		//	private void Init()
		//	{
		//		SingletonInit();

		//		_interactUIPool = new ObjectPool(_interactUIPrefab, _poolTransform );
		//		_interactUIPool.Init(5);


		//		//테스트
		//		CreateSharedUI();
		//	}

		//	private void EnsureEventSystem()
		//	{
		//		if (FindObjectOfType<EventSystem>() == null)
		//		{
		//			GameObject eventSystem = new GameObject("EventSystem",
		//				typeof(EventSystem),
		//				typeof(InputSystemUIInputModule)
		//			);

		//			eventSystem.transform.SetParent(RootUI.transform); // UI_Root 아래로 넣음
		//			Debug.LogWarning("EventSystem이 존재하지 않아 자동 생성됨");
		//		}
		//	}

		//	public void SetCanvas(GameObject uiGameObject)
		//	{
		//		Canvas canvas = uiGameObject.GetComponent<Canvas>();

		//		//렌더 - 오버레이
		//		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		//		//override sorting - true
		//		canvas.overrideSorting = true;

		//		canvas.sortingOrder = _order;
		//		_order++;

		//	}

		//	public T ShowUI<T>(string prefabPath) where T : BaseUI
		//	{
		//		if (string.IsNullOrEmpty(prefabPath))
		//		{
		//			Debug.LogError("프리팹 경로를 지정해주세요.");
		//			return null;
		//		}

		//		GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/{prefabPath}");

		//		if (prefab == null)
		//		{
		//			Debug.LogError("프리팹 경로 오류");
		//			return null;
		//		}

		//		T ui = Instantiate(prefab, RootUI.transform).GetComponent<T>();
		//		_UIStack.Push(ui);


		//		IsUIActive.Value = true;

		//		return ui;
		//	}

		//	public T ShowUI<T>(BaseUI uiPrefab) where T : BaseUI
		//	{
		//		if (uiPrefab == null) return null;
		//		T ui = Instantiate(uiPrefab, RootUI.transform).GetComponent<T>();
		//		_UIStack.Push(ui);

		//		IsUIActive.Value = true;

		//		return ui;
		//	}

		//	public void CloseUI(BaseUI ui)
		//	{
		//		if (_UIStack.Count == 0) return;
		//		if (_UIStack.Peek() != ui)
		//		{
		//			Debug.Log("팝업이 일치하지 않습니다.");
		//			return;
		//		}

		//		BaseUI popUI = _UIStack.Pop();
		//		Destroy(popUI.gameObject);
		//		_order--;

		//		if (_UIStack.Count == 0)
		//			IsUIActive.Value = false;

		//	}

		//	public void CloseAllUI()
		//	{
		//		while (_UIStack.Count != 0)
		//			CloseUI(_UIStack.Peek());

		//		Debug.Log("열려있는 UI를 모두 닫았습니다.");
		//	}

		//	public SharedUI CreateSharedUI()
		//	{
		//		if (sharedUI != null) return null;
		//		sharedUI = Instantiate(sharedUIPrefab);
		//		sharedUI.transform.SetParent(this.transform);
		//		return sharedUI;
		//	}

		//	public void DestroySharedUI()
		//	{
		//		if(sharedUI==null) return;

		//		Destroy(sharedUI.gameObject);
		//		sharedUI = null;
		//	}

	}
}