using System;
using UnityEngine;

namespace Node
{
    public class ValueNode<T> : BaseNode
    {
        [Header("Value")]
        [SerializeField] protected T _value;
        private DataOutPort<T> _dataOutPort;

        [Header("Camera Setting")]
        [SerializeField] private Canvas _canvas;
        private void Awake()
        {
            _canvas.worldCamera = Camera.main;
            _dataOutPort = outputPorts[0] as DataOutPort<T>;
        }
        
        /// <summary>
        /// 데이터 노드의 값 설정 및 데이터 아웃 포트 값 설정
        /// </summary>
        /// <param name="input"></param>
        public void UpdateValue(T input)
        {
            _value = input;
            _dataOutPort.SetValue(input);
        }
        
        
        //실행과 무관환 노드.
        public override void Execute() { }
    }
}