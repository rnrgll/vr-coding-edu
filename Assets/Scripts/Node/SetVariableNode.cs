using System;
using System.Collections;
using Managers;
using TMPro;
using UnityEngine;

namespace Node
{
    public class SetVariableNode<T> : BaseNode, IFlowNode
    {
        [SerializeField] private DataInPort<T> dataInPort;
        [SerializeField] private DataOutPort<T> dataOutPort;
        [SerializeField] private FlowPort nextFlow;
        
        [SerializeField] private TMP_Dropdown dropdown;
        private string variableName;
        private T variableValue;

        public IEnumerator Execute()
        {
            if (!dataInPort.IsConnected)
            {
                Debug.Log("[PortError] 입력 포트가 연결되지 않았습니다.");
                //todo: compileError
                // NodeManager.Instance.SetCompileError(true, "port");
                yield break;
            }

            if (dropdown.value == 0)
            {
                //todo: nodemanager 
                //NodeManager.Instance.SetCompileError(true, "No variable selected.");
                yield break;
            }
            
            variableName = dropdown.options[dropdown.value].text;
            yield return dataInPort.FetchData();
            variableValue = dataInPort.Value;
            Manager.Value.SetValue(variableName, typeof(T), variableValue);
            dataOutPort.SetValue(variableValue);
        }
        public FlowPort NextFlow()
        {
            return nextFlow;
        }
    }
}