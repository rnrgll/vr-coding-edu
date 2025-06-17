using System;
using System.Collections;
using Managers;
using UnityEngine;

namespace Node
{
    public class ActionNode_Say : BaseNode, IFlowNode
    {
        private FlexibleDataInPort dataInPort;
        [SerializeField] private string message;
        [SerializeField] private float duration = 2f;
        private void Start()
        {
            foreach (Port inputPort in inputPorts)
            {
                if (inputPort is FlexibleDataInPort dataPort)
                {
                    dataInPort = dataPort;
                    break;
                }
            }
        }
        
        public IEnumerator Execute()
        {
            if (!dataInPort.IsConnected)
            {
                Debug.Log("[PortError] 입력 포트가 연결되지 않았습니다.");
                Manager.Node.SetCompileError("port is not connected");
                yield break;
            }
            yield return dataInPort.FetchData();
            message = dataInPort.Value?.ToString();
            
            yield return Manager.Cat.Controller.Say(message, duration);
        }
        public FlowPort NextFlow()
        {
            return outputPorts[0] as FlowPort;
        }
    }

}