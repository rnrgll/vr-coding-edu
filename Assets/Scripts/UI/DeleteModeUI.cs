using System;
using Managers;
using Unity.VisualScripting;
using UnityEngine;

namespace NodeUI
{
    public class DeleteModeUI : MonoBehaviour
    {
        
        private void OnEnable()
        {
            Manager.Node.OnDeleteModeChanged += SetUIActive;
        }

        private void OnDisable()
        {
            Manager.Node.OnDeleteModeChanged -= SetUIActive;
        }

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void SetUIActive(bool isDeleteMode)
        {
            gameObject.SetActive(isDeleteMode);
        }
    }
}