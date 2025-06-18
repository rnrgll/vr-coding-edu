using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.SpatialKeyboard;

namespace Node
{
    public class ValueNode_String : ValueNode<string>
    {
        [Header("UI")]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private XRKeyboardDisplay _xrKeyboard;
        
        private void OnEnable()
        {
            _inputField.onEndEdit.AddListener(UpdateValue);
            _xrKeyboard.onTextSubmitted.AddListener(UpdateValue);
            _xrKeyboard.onKeyboardFocusChanged.AddListener(OnKeyboardFoucsChanged);
        }

        private void OnDisable()
        {
            _inputField.onValueChanged.RemoveListener(UpdateValue);
            _xrKeyboard.onTextSubmitted.RemoveListener(UpdateValue);
            _xrKeyboard.onKeyboardFocusChanged.RemoveListener(OnKeyboardFoucsChanged);
        }
        
        public void OnKeyboardFoucsChanged()
        {
            if (!_inputField.wasCanceled) // 예: 사용자가 ESC로 닫은 경우 제외
            {
                _inputField.text = _value; // 포커스 잃으면 원래 값 복원
            }
        }
    }
}