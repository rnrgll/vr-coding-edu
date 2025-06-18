using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Samples.SpatialKeyboard;

namespace Node
{
    public class ValueNode_Float : ValueNode<float>
    {
        [Header("UI")]
        // [SerializeField] private Slider _slider;
        // [SerializeField] private TMP_Text _tmpText;
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private float _minValue = -99f;
        [SerializeField] private float _maxValue = 99f;
        [SerializeField] private XRKeyboardDisplay _xrKeyboard;

        private void Start()
        {
            _inputField.contentType = TMP_InputField.ContentType.Standard;
            // _slider.minValue = _minValue;
            // _slider.maxValue = _maxValue;
            // _slider.wholeNumbers = false; //소수도 가능하게 설정
            //OnSliderChanged(0f);
            
            OnInputEnded("0");
        }
        
        private void OnEnable()
        {
            _inputField.onEndEdit.AddListener(OnInputEnded);
             _xrKeyboard.onTextSubmitted.AddListener(OnInputEnded);
             _xrKeyboard.onKeyboardFocusChanged.AddListener(OnKeyboardFoucsChanged);
            //_slider.onValueChanged.AddListener(OnSliderChanged);
        }

        private void OnDisable()
        {
            _inputField.onEndEdit.RemoveListener(OnInputEnded);
            _xrKeyboard.onTextSubmitted.RemoveListener(OnInputEnded);
            _xrKeyboard.onKeyboardFocusChanged.RemoveListener(OnKeyboardFoucsChanged);
            //_slider.onValueChanged.RemoveListener(OnSliderChanged);
        }
        
        private void OnInputEnded(string text)
        {
            if (float.TryParse(text, out float result))
            {
                float clamped = Mathf.Clamp(result, _minValue, _maxValue);
                clamped = Mathf.Floor(clamped * 100f) / 100f;
                
                UpdateValue(clamped);
                //_slider.SetValueWithoutNotify(clamped); // 슬라이더와 동기화
                _inputField.text = clamped.ToString("F2");
                
            }
            else
            {
                // 되돌리기
                _inputField.text = _value.ToString("F2");
        
            }
        }

        // private void OnSliderChanged(float value)
        // {
        //     float rounded = Mathf.Round(value * 100f) / 100f;
        //     UpdateValue(rounded);
        //     _tmpText.text = rounded.ToString("F2");
        //     
        //     //_inputField.SetTextWithoutNotify(rounded.ToString("F2"));
        // }
        public void OnKeyboardFoucsChanged()
        {
            if (!_inputField.wasCanceled) // 예: 사용자가 ESC로 닫은 경우 제외
            {
                _inputField.text = _value.ToString("F2"); // 포커스 잃으면 원래 값 복원
            }
        }
    }
}