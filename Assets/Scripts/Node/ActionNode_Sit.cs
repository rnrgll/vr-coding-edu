using System.Collections;
using Managers;
using UnityEngine;

namespace Node
{
    public class ActionNode_Sit : BaseNode, IFlowNode
    {
        public IEnumerator Execute()
        {
            yield return Manager.Cat.Controller.PlaySit();
        }
        public FlowPort NextFlow()
        {
            return outputPorts[0] as FlowPort;
        }
    }
}