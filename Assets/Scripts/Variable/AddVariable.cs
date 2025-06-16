using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Variable
{
    public class AddVariable : MonoBehaviour
    {
        [Header("Variable Type")]
        [SerializeField] private Define.DataType _type;

        [Header("UI")]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private GameObject _modalPanel;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _errorText;

        private void Start()
        {
            _button.onClick.AddListener(OnClickAdd);
            _errorText.enabled = false;
        }
        
        private void OnClickAdd()
        {
            string varName = _inputField.text.Trim();

            if (string.IsNullOrEmpty(varName))
            {
                ShowError("Please enter a variable name.");
                return;
            }

            if (Manager.Value.Exists(varName, _type))
            {
                ShowError($"A variable named '{varName}' already exists.");
                return;
            }
            Manager.Value.AddValue(_type, varName);

            CloseModal();
        }
        private void ShowError(string message)
        {
            _errorText.text = message;
            _errorText.enabled = true;
        }
        
        private void HideError()
        {
            _errorText.text = "";
            _errorText.enabled = false;
        }

        private void CloseModal()
        {
            HideError();
            _modalPanel?.SetActive(false);
        }
    }
}