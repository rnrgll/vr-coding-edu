using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;

namespace Variable
{
    public class VariableDropDown : MonoBehaviour
    {
        [SerializeField] private Define.DataType variableType;
        private TMP_Dropdown _dropdown;
        
        private void Start()
        {
            _dropdown = GetComponent<TMP_Dropdown>();
    
            Manager.Value.OnUpdateValues.AddListener(UpdateOptions);
            LoadAllVariableNames();  // 초기 로딩
        }

        private void OnDestroy()
        {
            Manager.Value.OnUpdateValues.RemoveListener(UpdateOptions);
        }
        
        private void LoadAllVariableNames()
        {
            _dropdown.options.Clear();
            _dropdown.options.Add(new TMP_Dropdown.OptionData("None Selected"));

            switch (variableType)
            {
                case Define.DataType.Int:
                    AddOptionsFromDict(Manager.Value.IntValues);
                    break;
                case Define.DataType.Bool:
                    AddOptionsFromDict(Manager.Value.BoolValues);
                    break;
                case Define.DataType.String:
                    AddOptionsFromDict(Manager.Value.StrValues);
                    break;
            }

            _dropdown.SetValueWithoutNotify(0);
        }

        private void UpdateOptions()
        {
            LoadAllVariableNames();
        }
        private void AddOptionsFromDict<T>(Dictionary<string, ValueEntry<T>> dict)
        {
            foreach (var pair in dict)
            {
                _dropdown.options.Add(new TMP_Dropdown.OptionData(pair.Key));
            }
        }
        public string GetSelectedVariableName()
        {
            int index = _dropdown.value;
            if (index <= 0 || index >= _dropdown.options.Count)
                return null;

            return _dropdown.options[index].text;
        }
    }
}