using System.Collections;
using Managers;
using UnityEngine;

namespace Node
{
    public class ActionNode_Rotate : BaseNode, IFlowNode
    {
        [SerializeField] private float angle = 90f;
        [SerializeField] private float duration = 1f;

        public IEnumerator Execute()
        {
            yield return Manager.Cat.Controller.Rotate(angle, duration);
        }

        public void SetAngle(float angle)
        {
            this.angle = angle;
        }
        public FlowPort NextFlow()
        {
            return outputPorts[0] as FlowPort;
        }
    }
}