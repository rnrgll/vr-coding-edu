using UnityEngine;

namespace Variable
{
    public class ValueEntry<T> : IVariableEntry
    {
        public string varName;
        public T varValue;
        public bool isInit;

        public string Name => varName;
        public bool IsInitialized { get => isInit; set => isInit = value; }
        public object GetValue() => varValue;

        public void SetValue(object value)
        {
            if (value is T typedValue)
                this.varValue = typedValue; 
        }
    }
}