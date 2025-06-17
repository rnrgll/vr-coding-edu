using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DeleteButton : MonoBehaviour
{
    [SerializeField] private XRSimpleInteractable interactable;

    [SerializeField] private
        bool isDeleteMode = false;
    
    
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
        isDeleteMode = !isDeleteMode;
        Manager.Node.DeleteMode = isDeleteMode;
        Debug.Log($"DeleteMode : {isDeleteMode}");
    }
}
