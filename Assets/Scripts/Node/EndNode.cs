using UnityEngine;

namespace Node
{
    public class EndNode : BaseNode
    {
        public override void Execute()
        {
            Debug.Log("Execute 호출");
        }
    }
}