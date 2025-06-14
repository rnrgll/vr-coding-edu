using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNode : MonoBehaviour
{
    public string nodeName;
    public string nodeId;

    public List<Port> inputPorts;
    public List<Port> outputPorts;

    public bool errorFlag = true;
}
