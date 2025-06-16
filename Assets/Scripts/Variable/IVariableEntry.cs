using UnityEngine;

namespace Variable
{
    public interface IVariableEntry
    {
        string Name { get; }
        bool IsInitialized { get; set; }
        object GetValue();
        void SetValue(object value);
    }
}