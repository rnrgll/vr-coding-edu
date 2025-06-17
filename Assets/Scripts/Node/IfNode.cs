using System.Collections;
using Managers;
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
                Manager.Node.SetCompileError("The green outFlow port must be connected.");
                yield break;
            }

            if (!trueFlow.IsConnected && !falseFlow.IsConnected)
            {
                Manager.Node.SetCompileError("At least one of the true/false outFlow ports must be connected.");
                yield break;
            }

            if (!conditionInPort.IsConnected)
            {
                Manager.Node.SetCompileError("Condition input is not connected.");
                yield break;
            }
            yield return conditionInPort.FetchData();
            bool condition = conditionInPort.Value;

            _selectedBranch = condition ? trueFlow : falseFlow;
            
            BaseNode next = _selectedBranch.ConnectedPort.ParentNode;
            yield return Manager.Node.Flow.Run(next);

        }

        public FlowPort NextFlow()
        {
            return nextFlow;
        }
    }
}