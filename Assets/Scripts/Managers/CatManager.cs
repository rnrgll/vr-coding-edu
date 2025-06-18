using DesignPattern;
using Suriyun;
using UnityEngine;

namespace Managers
{
    public class CatManager : Singleton<CatManager>
    {
        public CatController Controller;
        public AnimatorController Anim;
        private void Awake() => Init();

        private void Init()
        {
            SingletonInit();
        }

    }
}