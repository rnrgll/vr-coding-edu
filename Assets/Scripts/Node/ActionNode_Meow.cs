using System.Collections;
using Managers;
using UnityEngine;

namespace Node
{
    public class ActionNode_Meow : BaseNode, IFlowNode
    {
        public IEnumerator Execute()
        {
            yield return Manager.Cat.Controller.PlayMeow();
        }
    }
}