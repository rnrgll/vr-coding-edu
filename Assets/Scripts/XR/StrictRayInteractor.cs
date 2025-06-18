using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StrictRayInteractor : XRRayInteractor
{
    public override void GetValidTargets(List<IXRInteractable> targets)
    {
        base.GetValidTargets(targets);

        // Raycast 결과로 실제로 맞은 콜라이더의 GameObject를 가져옵니다
        if (TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            
            // 우선, "정확히 맞은 오브젝트"에 Grab이 있으면 그걸로만 제한
            var exact = hitObject.GetComponent<IXRInteractable>();
            if (exact != null)
            {
                targets.Clear();
                targets.Add(exact);
                return;
            }
        }
    }
}