using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using DesignPattern;
using Node;
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

        
        //delete--------
        private bool deleteMode = false;
        public event Action<bool> OnDeleteModeChanged;

        public bool DeleteMode
        {
            get
            {
                return deleteMode;

            }

            set
            {
                    deleteMode = value;
                    OnDeleteModeChanged?.Invoke(value);

            }


        }
        
        private void Awake() => Init();

        private void Init()
        {
            
            SingletonInit();
        }

        public void Run(RunButton runButton)
        {
            Manager.Value.Clear();
            runProgram = StartCoroutine(RunProgram());
        }

        private IEnumerator RunProgram()
        {
            var startNodes = GameObject.FindGameObjectsWithTag("startNode");

            if (startNodes.Length == 0)
            {
                SetCompileError("Start node not found.");
                yield break;
            }

            if (startNodes.Length > 1)
            {
                SetCompileError("Only one start node is allowed.");
                yield break;
            }

            
            var startNode = startNodes[0].GetComponent<BaseNode>();
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