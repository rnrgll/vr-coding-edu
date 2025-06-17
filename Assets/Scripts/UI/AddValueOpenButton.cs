using Managers;
using UnityEngine;

namespace NodeUI
{
    public class AddValueOpenButton : MonoBehaviour
    {
        public void OpenUI()
        {
            Manager.UI.OpenAddValueUI();
        }
    }
}