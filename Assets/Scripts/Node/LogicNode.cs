using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Node
{
    public class LogicNode : BaseNode, IDataNode
    {
        public enum LogicOperator
        {
            And,
            Or,
        }
        [Header("Operation Type")] [SerializeField]
        private LogicOperator logicType;
        
        private DataOutPort<bool> dataOutPort;
        private bool result;
        
        private void Start()
        {
            dataOutPort = outputPorts[0] as DataOutPort<bool>;
        }
        
        public IEnumerator ProcessData()
        {
            var dataInPort1 = inputPorts[0] as DataInPort<bool>;
            var dataInPort2 = inputPorts[1] as DataInPort<bool>;

            if (!dataInPort1.IsConnected || !dataInPort2.IsConnected)
            {
                Debug.Log("[PortError] 입력 포트가 연결되지 않았습니다.");
                //todo: compileError
                // NodeManager.Instance.SetCompileError(true, "port");
                yield break;
            }

            yield return dataInPort1.FetchData();
            yield return dataInPort2.FetchData();
            

            result = logicType switch
            {
                LogicOperator.And => dataInPort1.Value && dataInPort2.Value,
                LogicOperator.Or => dataInPort1.Value || dataInPort2.Value,
                _ => false
            };
            
            dataOutPort.SetValue(result);

        }
    }
}