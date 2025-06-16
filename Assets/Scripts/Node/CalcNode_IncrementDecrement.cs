using System.Collections;
using TMPro;
using UnityEngine;

namespace Node
{
    public class CalcNode_IncrementDecrement : BaseNode, IDataNode
    {
        public enum Operator
        {
            Decrement,
            Increment,
        }

        [Header("Operation Type")] [SerializeField]
        private Operator _operatorType;
        [SerializeField] private TMP_Text _outputLabel;
        [SerializeField] private string _outputLabelFormat = "X {0} 1";
        
        
        private DataOutPort<int> _dataOutPort;
        private int _result;
        
        private void Start()
        {
            _dataOutPort = outputPorts[0] as DataOutPort<int>;
        }

        public void SetOperationType(int type)
        {
            _operatorType = (Operator)type;
            _outputLabel.text = string.Format(_outputLabelFormat, (_operatorType==Operator.Increment?"+":"-"));
            
        }
        
        public IEnumerator ProcessData()
        {
            DataInPort<int> dataInPort = inputPorts[0] as DataInPort<int>;
            
            if (!dataInPort.IsConnected)
            {
                Debug.Log("[PortError] 입력 포트가 연결되지 않았습니다.");
                //todo: compileError
                // NodeManager.Instance.SetCompileError(true, "port");
                yield break;
            }
            yield return dataInPort.FetchData();

            _result = _operatorType switch
            {
                Operator.Increment => dataInPort.Value + 1,
                Operator.Decrement => dataInPort.Value - 1,
                _ => dataInPort.Value
            };
            
            _dataOutPort.SetValue(_result);

        }
    }
}