using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace NodeUI
{
    public class PortLabelUpdater : MonoBehaviour
    {
        [System.Serializable]
        public class PortLabel
        {
            public Port port;
            public TMP_Text label;
            public string defaultText;
            public string format;

            public void UpdateLabel(bool isConnected)
            {
                //if(isConnected)
                    //label.text = string.Format(format, port.v)
            }
        }

        [SerializeField] public List<PortLabel> labels;

        private void OnEnable()
        {
            foreach (var portLabel in labels)
            {
                // if(portLabel.port!=null)
                //     portLabel.port.OnConnectionChanged += 
            }
        }

        private void UpdateLabel(bool isConnected)
        {
            //if(isConnected)
                //label
        }
    }
}