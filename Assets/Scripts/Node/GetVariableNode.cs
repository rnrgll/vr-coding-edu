using System.Collections;
using Managers;
using TMPro;
using UnityEngine;

namespace Node
{
    public class GetVariableNode : BaseNode, IDataNode
    {
        [SerializeField] private Define.DataType dataType;
        [SerializeField] private Port dataOutPort;
        
        [SerializeField] private TMP_Dropdown dropdown;
        private string variableName;
        public IEnumerator ProcessData()
        {
            if (dropdown.value == 0)
            {
                //todo:nodemanager
                Debug.LogWarning("[GetVariableNode] No variable selected.");
                Manager.Node.SetCompileError("No variable selected.");
                yield break;
            }
            variableName = dropdown.options[dropdown.value].text;
            switch (dataType)
            {
                case Define.DataType.Int:
                {
                    if(!Manager.Value.Exists(variableName, Define.DataType.Int))
                        yield break;
                    var intOut = dataOutPort as DataOutPort<int>;
                    intOut.SetValue((int)Manager.Value.GetValue(variableName,Define.DataType.Int));
                    break;
                }

                case Define.DataType.Bool:
                {
                    if(!Manager.Value.Exists(variableName, Define.DataType.Bool))
                        yield break;
                    var intOut = dataOutPort as DataOutPort<bool>;
                    intOut.SetValue((bool)Manager.Value.GetValue(variableName,Define.DataType.Bool));
                    break;
                }
                case Define.DataType.String:
                {
                    if(!Manager.Value.Exists(variableName, Define.DataType.String))
                        yield break;
                    var intOut = dataOutPort as DataOutPort<string>;
                    intOut.SetValue((string)Manager.Value.GetValue(variableName,Define.DataType.String));
                    break;
                }
            }

            yield return null;
        }
    }
}