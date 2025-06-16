using System.Collections;
using Runner;
using UnityEngine;

namespace Node
{
    public class IfNode : BaseNode, IFlowNode
    {
        [Header("Ports")]
        [SerializeField] private DataInPort<bool> conditionInPort;
        [SerializeField] private FlowPort trueFlow;
        [SerializeField] private FlowPort falseFlow;
        [SerializeField] private FlowPort nextFlow;
        private FlowPort _selectedBranch;

        
        public IEnumerator Execute()
        {
            if (!nextFlow.IsConnected)
            {
                //todo:nodemanager
                //NodeManager.Instance.SetCompileError(true, "The green outFlow port must be connected.");
                yield break;
            }

            if (!trueFlow.IsConnected && !falseFlow.IsConnected)
            {
                //todo:nodemanager
                //NodeManager.Instance.SetCompileError(true, "At least one of the true/false outFlow ports must be connected.");
                yield break;
            }

            if (!conditionInPort.IsConnected)
            {
                //todo:nodemanager
                //NodeManager.Instance.SetCompileError(true, "Condition input is not connected.");
                yield break;
            }
            yield return conditionInPort.FetchData();
            bool condition = conditionInPort.Value;

            _selectedBranch = condition ? trueFlow : falseFlow;
            
            BaseNode next = _selectedBranch.ConnectedPort.ParentNode;
            yield return FlowRunner.Instance.Run(next);
            
            

        }

        public FlowPort NextFlow()
        {
            return nextFlow;
        }
    }
}