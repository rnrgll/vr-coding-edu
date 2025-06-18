using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RestButton : MonoBehaviour
{
    [SerializeField] private Transform _nodespawnPoint;
    [SerializeField] private XRSimpleInteractable interactable;
    
    private void OnEnable()
    {
        interactable.selectEntered.AddListener(OnButtonPressed);
    }

    private void OnDisable()
    {
        interactable.selectEntered.RemoveListener(OnButtonPressed);
    }
    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        // 자식 오브젝트들을 모두 순회하며 삭제
        foreach (Transform child in _nodespawnPoint)
        {
            Destroy(child.gameObject);
        }
    }
    
    
}
