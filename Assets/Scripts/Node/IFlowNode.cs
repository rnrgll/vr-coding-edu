using System.Collections;

namespace Node
{
    public interface IFlowNode
    {
        IEnumerator Execute();
        FlowPort NextFlow();
    }
}