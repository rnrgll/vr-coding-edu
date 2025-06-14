using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Node
{
    public class LogicNode_Not : BaseNode, IDataNode
    {
        //port
        private DataInPort<bool> _dataInPort;
        private DataOutPort<bool> _dataOutPort;
        
        
        //input
        private bool _inputVal;
        private bool _result;

        private void Start()
        {
            _dataInPort  = inputPorts[0] as DataInPort<bool>;
            _dataOutPort = outputPorts[0] as DataOutPort<bool>;
        }
        
        public IEnumerator ProcessData()
        {
            //1. 노드가 연결되어 있는지 확인한다. 연결되어 있지 않다면 컴파일 에러!
            //2. 연결되어 있는 경우 데이터 가져온다.
            if (!_dataInPort.IsConnected)
            {
                Debug.Log("[PortError] 입력 포트가 연결되지 않았습니다.");
                //todo: compileError
                // NodeManager.Instance.SetCompileError(true, "port");
                yield break;
            }
            yield return _dataInPort.FetchData();
            _inputVal = _dataInPort.Value;
            _result = !_inputVal;
            _dataOutPort.SetValue(_result);
            

        }
    }
}