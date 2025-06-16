using System;
using System.Collections.Generic;
using DesignPattern;
using UnityEngine;
using UnityEngine.Events;
using Variable;
using static Define;

namespace Managers
{
    public class ValueManager : Singleton<ValueManager>
    {
        public Dictionary<string, ValueEntry<int>> IntValues = new();
        public Dictionary<string, ValueEntry<bool>> BoolValues = new();
        public Dictionary<string, ValueEntry<string>> StrValues = new();

        public UnityEvent OnUpdateValues;

        public void Reset()
        {
            IntValues.Clear();
            BoolValues.Clear();
            StrValues.Clear();
        }

        public void AddValue(DataType type, string name, object defaultValue = null)
        {
            switch (type)
            {
                case DataType.Int:
                    IntValues[name] = new ValueEntry<int>
                    {
                        varName = name,
                        varValue = defaultValue is int i ? i : 0
                    };
                    break;
                case DataType.Bool:
                    BoolValues[name] = new ValueEntry<bool>
                    {
                        varName = name,
                        varValue = defaultValue is bool b ? b : true
                    };
                    break;
                case DataType.String:
                    StrValues[name] = new ValueEntry<string>
                    {
                        varName = name,
                        varValue = defaultValue as string ?? ""
                    };
                    break;
                default:
                    throw new ArgumentException($"Unsupported DataType: {type}");
            }

            OnUpdateValues?.Invoke();
        }

        public bool Exists(string name, DataType type)
        {
            return type switch
            {
                DataType.Int => IntValues.ContainsKey(name),
                DataType.Bool => BoolValues.ContainsKey(name),
                DataType.String => StrValues.ContainsKey(name),
                _ => false
            };
        }

        public object GetValue(string name, DataType type)
        {
            return type switch
            {
                DataType.Int => IntValues[name].varValue,
                DataType.Bool => BoolValues[name].varValue,
                DataType.String => StrValues[name].varValue,
                _ => throw new ArgumentException($"Unsupported DataType: {type}")
            };
        }

        public void SetValue(string name, DataType type, object value)
        {
            switch (type)
            {
                case DataType.Int:
                    IntValues[name].varValue = (int)value;
                    break;
                case DataType.Bool:
                    BoolValues[name].varValue = (bool)value;
                    break;
                case DataType.String:
                    StrValues[name].varValue = (string)value;
                    break;
                default:
                    throw new ArgumentException($"Unsupported DataType: {type}");
            }
        }
        
        public void SetValue(string name, Type type, object value)
        {
            if (type == typeof(int))
                IntValues[name].varValue = (int)value;
            else if (type == typeof(bool))
                BoolValues[name].varValue = (bool)value;
            else if (type == typeof(string))
                StrValues[name].varValue = (string)value;
            else
                throw new ArgumentException($"Unsupported Type: {type}");
        }
    }
}