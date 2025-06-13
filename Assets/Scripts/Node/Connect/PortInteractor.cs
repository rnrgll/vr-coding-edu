using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PortInteractor : MonoBehaviour
{
    public DragLine dragLinePrefab;
    [SerializeField] private Transform fixedBase;
    [SerializeField] private Transform dragHandle;

    private DragLine curentLine;
    private XRGrabInteractable grabInteractable;


    private void Awake()
    {
        dragHandle = transform;
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabStart);
        grabInteractable.selectExited.AddListener(OnGragEnd);
    }

    private void OnGrabStart(SelectEnterEventArgs args)
    {
        Debug.Log("OnGrabStart");

        if (curentLine != null) return;
        curentLine = Instantiate(dragLinePrefab, fixedBase.position, Quaternion.identity);
        
        Debug.Log("curentLine != null line 생성");
        curentLine.Init(fixedBase, dragHandle);
    }

    private void OnGragEnd(SelectExitEventArgs args)
    {
        if (curentLine == null) return;
        //todo: 연결된 포트가 있다면 연결을 완료하고, 없다면 라인을 제거
        curentLine.DestroyLine();
        curentLine = null;
    }
}