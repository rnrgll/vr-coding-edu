using System;
using System.Collections.Generic;
using System.Data;
using DesignPattern;

namespace Managers
{

    public class DataManager : Singleton<DataManager>
    {
        
        private void Awake() => Init();

        private void Init()
        {
            
            SingletonInit();
        }
        

    
    }
}