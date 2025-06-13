using static Define;

public class DataInPort<T> : DataPort<T>
{

    protected override void Init()
    {
        base.Init();
        Direction = PortDirection.In; // 입력 포트로 설정
    }

    public void FetchData()
    {

        if (ConnectedPort != null)
        {
            value = ConnectedDataPort.Value;
        }
    }
}

