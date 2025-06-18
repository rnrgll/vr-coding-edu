using TMPro;
using UnityEngine;

namespace NodeUI
{
    public class ResultModalUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _runState;
        [SerializeField] private TMP_Text _errorText;

        public void SetErrorMsg(string error)
        {
            _runState.text = "Error: Execution stopped.";
            _errorText.text = error;
            _errorText.enabled = true;
            gameObject.SetActive(true);
        }

        public void SetComplete()
        {
            _runState.text = "Execution complete";
            _errorText.enabled = false;
            gameObject.SetActive(true);
        }
        
    }
}