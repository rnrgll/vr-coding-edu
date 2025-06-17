using System.Collections;
using System.Collections.Generic;
using Managers;
using Node;
using UnityEngine;

public class StringConcatNode : BaseNode, IDataNode
{
    private FlexibleDataInPort dataInPort1;
    private FlexibleDataInPort dataInPort2;
    private DataOutPort<string> dataOutPort;
    private string result;
    void Start()
    {
        dataInPort1 = inputPorts[0] as FlexibleDataInPort;
        dataInPort2 = inputPorts[1] as FlexibleDataInPort;
        dataOutPort = outputPorts[0] as DataOutPort<string>;
    }

    public IEnumerator ProcessData()
    {
        if (!dataInPort1.IsConnected || !dataInPort2.IsConnected)
        {
            Debug.Log("[PortError] 입력 포트가 연결되지 않았습니다.");
            Manager.Node.SetCompileError("port is not connected");
            yield break;
        }
        yield return dataInPort1.FetchData();
        yield return dataInPort2.FetchData();

        string str1 = dataInPort1.Value?.ToString();
        string str2 = dataInPort2.Value?.ToString();

        result = str1 + str2;
        dataOutPort.SetValue(result);
    }
}
