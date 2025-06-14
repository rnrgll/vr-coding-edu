using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.SpatialKeyboard;

namespace Node
{
    public class ValueNode_Int : ValueNode<int>
    {
        [Header("UI")]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Button _upButton;
        [SerializeField] private Button _downButton;
        [SerializeField] private int _minValue = -99;
        [SerializeField] private int _maxValue = 99;
        [SerializeField] private XRKeyboardDisplay _xrKeyboard;
        
        private int _lastValidValue = 0;
        
        private void OnEnable()
        {
            _inputField.onEndEdit.AddListener(OnInputEnded);
             _xrKeyboard.onTextSubmitted.AddListener(OnInputEnded);
            _xrKeyboard.onKeyboardFocusChanged.AddListener(OnKeyboardFoucsChanged);
            _upButton.onClick.AddListener(OnValueUp);
            _downButton.onClick.AddListener(OnValueDown);

        }
        private void OnDisable()
        {
            _inputField.onEndEdit.RemoveListener(OnInputEnded);
            _xrKeyboard.onTextSubmitted.RemoveListener(OnInputEnded);
            _xrKeyboard.onKeyboardFocusChanged.RemoveListener(OnKeyboardFoucsChanged);
            _upButton.onClick.RemoveListener(OnValueUp);
            _downButton.onClick.RemoveListener(OnValueDown);

        }
        
        public void OnValueUp()
        {
            if (_value < _maxValue)
            {
                int newVal = _value + 1;
                UpdateValue(newVal);
                _inputField.text = newVal.ToString();
            }
        }

        public void OnValueDown()
        {
            if (_value > _minValue)
            {
                int newVal = _value - 1;
                UpdateValue(newVal);
                _inputField.text =newVal.ToString();
            }
        }

        public void OnInputEnded(string text)
        {
            if (int.TryParse(text, out int result))
            {
                int clamped = Mathf.Clamp(result, _minValue, _maxValue);
                
                UpdateValue(clamped);
                _inputField.text = clamped.ToString();
            }
            else
            {
                _inputField.text = _value.ToString();
            }
        }

        public void OnKeyboardFoucsChanged()
        {
            if (!_inputField.wasCanceled) // 예: 사용자가 ESC로 닫은 경우 제외
            {
                _inputField.text = _value.ToString(); // 포커스 잃으면 원래 값 복원
            }
        }
        
    }
}