using System.Collections;
using Managers;
using UnityEngine;

namespace Node
{
    public class ActionNode_Punch : BaseNode, IFlowNode
    {
        public IEnumerator Execute()
        {
            yield return Manager.Cat.Controller.PlayPunch();

        }
        public FlowPort NextFlow()
        {
            return outputPorts[0] as FlowPort;
        }
    }
}