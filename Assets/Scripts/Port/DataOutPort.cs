using static Define;

public class DataOutPort<T> : DataPort<T>
{

    protected override void Init()
    {
        base.Init();
        Direction = PortDirection.Out; // 입력 포트로 설정
    }
    public void SetValue(T value)
    {
        this.value = value;
    }
}

