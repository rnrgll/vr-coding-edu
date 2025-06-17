using Managers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace NodeUI
{
    public class RunButton : MonoBehaviour
    {
        [SerializeField] private XRSimpleInteractable interactable;

        [SerializeField] private
         bool isClicked = false;
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
            if (isClicked) return;
            isClicked = true;
            Debug.Log("button pressed");
            Manager.Node.Run(this);
        }

        public void ResetClick()
        {
            isClicked = false;
        }
    }
}