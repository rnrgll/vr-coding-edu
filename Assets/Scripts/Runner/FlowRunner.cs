using System;
using System.Collections;
using DesignPattern;
using Node;
using UnityEngine;

namespace Runner
{
    public class FlowRunner : Singleton<FlowRunner>
    {
        private void Awake()
        {
            SingletonInit();
        }

        public IEnumerator Run(BaseNode startNode)
        {
            if (startNode == null) yield break;
            if (startNode is not IFlowNode node)
            {
                yield break;
            }

            yield return node.Execute();

            FlowPort nextPort = node.NextFlow();
            if (nextPort != null && nextPort.ConnectedPort != null)
            {
                BaseNode nextNode = nextPort.ConnectedPort.ParentNode;
                yield return Run(nextNode);
            }
        }
    }
}