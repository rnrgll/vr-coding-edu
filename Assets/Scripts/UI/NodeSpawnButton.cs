using UnityEngine;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
namespace NodeUI
{
    public class NodeSpawnButton : MonoBehaviour
    {
        public GameObject nodePrefab;
        [SerializeField] private XRSimpleInteractable interactable;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Transform parent;
        private void OnEnable()
        {
            interactable.selectEntered.AddListener(SpawnNode);
        }

        private void OnDisable()
        {
            interactable.selectEntered.RemoveListener(SpawnNode);
        }
        
        private void SpawnNode(SelectEnterEventArgs args)
        {
            GameObject node = Instantiate(nodePrefab, spawnPoint.position, Quaternion.identity);
            node.transform.SetParent(parent);
        }
        
    }
}