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
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _closeButton;
        
        [SerializeField] private TMP_Text _errorText;

        
        private void Start()
        {
            _addButton.onClick.AddListener(OnClickAdd);
            _closeButton.onClick.AddListener(CloseModal);
            _errorText.enabled = false;
        }

        private void OnDestroy()
        {
            _addButton.onClick.RemoveListener(OnClickAdd);
            _closeButton.onClick.RemoveListener(CloseModal);
        }

        private void OnClickAdd()
        {
            string varName = _inputField.text.Trim();

            // 1. 비어있는 이름
            if (string.IsNullOrEmpty(varName))
            {
                ShowError("Please enter a variable name.");
                return;
            }

            // 2. 유효한 변수명인지 검사 (정규식)
            if (!IsValidVariableName(varName))
            {
                ShowError("Only letters, numbers, and underscores are allowed.\nIt must not start with a number.");
                return;
            }

            // 3. 중복 검사
            if (Manager.Value.Exists(varName, _type))
            {
                ShowError($"A variable named '{varName}' already exists.");
                return;
            }

            // 4. 성공 시 등록
            Manager.Value.AddValue(_type, varName);
            CloseModal();
        }

        private bool IsValidVariableName(string name)
        {
            // ^ 시작은 문자 또는 _
            // [a-zA-Z_][a-zA-Z0-9_]*$
            return System.Text.RegularExpressions.Regex.IsMatch(name, @"^[a-zA-Z_][a-zA-Z0-9_]*$");
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
            _inputField.text = "";
            _modalPanel?.SetActive(false);
        }

        public void SetDataType(int typeNum)
        {
            _type = (Define.DataType)typeNum;
        }
    }
}