using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using DesignPattern;
using NodeUI;
using Runner;
using UnityEngine;

namespace Managers
{

    public class NodeManager : Singleton<NodeManager>
    {
        public FlowRunner Flow;
        
        [SerializeField] private ResultModalUI _resultModal;
        private Coroutine runProgram;
        
        
        private void Awake() => Init();

        private void Init()
        {
            
            SingletonInit();
        }

        public void Run(RunButton runButton)
        {
            runProgram = StartCoroutine(RunProgram());
        }

        private IEnumerator RunProgram()
        {
            var startNodeObj = GameObject.FindGameObjectWithTag("startNode");
            if (startNodeObj == null)
            {
                SetCompileError("Start node not found.");
                yield break;
            }
            
            var startNode = startNodeObj.GetComponent<BaseNode>();
            if (startNode == null)
            {
                SetCompileError("Start node component missing.");
                yield break;
            }
            
            yield return Flow.Run(startNode);

            _resultModal.SetComplete();

        }

        public void SetCompileError(string error)
        {
            StopCoroutine(runProgram);
            _resultModal.SetErrorMsg(error);
        }
    
    }
}