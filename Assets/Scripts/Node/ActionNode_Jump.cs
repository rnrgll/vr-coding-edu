using System.Collections;
using Managers;
using UnityEngine;

namespace Node
{
    public class ActionNode_Jump : BaseNode, IFlowNode
    {
        public IEnumerator Execute()
        {
            yield return Manager.Cat.Controller.PlayJump();
        }
    }
}