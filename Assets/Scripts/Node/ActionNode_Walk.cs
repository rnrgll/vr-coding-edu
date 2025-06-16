using System.Collections;
using Managers;
using UnityEngine;

namespace Node
{
    public class ActionNode_Walk : BaseNode, IFlowNode    
    {
        private DataInPort<int> dataInPort;
        
        [SerializeField] private float distance = 0f;
        [SerializeField] private float duration = 1f;
        
        private void Start()
        {
            foreach (Port inputPort in inputPorts)
            {
                if (inputPort is DataInPort<int> dataPort)
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
                //todo: compileError
                // NodeManager.Instance.SetCompileError(true, "port");
                yield break;
            }
            yield return dataInPort.FetchData();

            distance = dataInPort.Value;
            
            yield return Manager.Cat.Controller.MoveForward(distance, duration);
        }
        public FlowPort NextFlow()
        {
            return outputPorts[0] as FlowPort;
        }
    }
}