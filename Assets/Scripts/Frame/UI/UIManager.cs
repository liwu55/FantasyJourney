using System.Collections.Generic;
using Frame.UI;
using Frame.Utility;
using UnityEngine;
using Object = System.Object;

public class UIManager : SingleTonMonoAuto<UIManager>
{
    /// <summary>
    /// 所有的ui模块
    /// </summary>
    private Dictionary<string, UIModuleBase> _uiModuleBases;

    private Stack<UIModuleBase> uiModuleStack;

    private Transform tranCanvas;

    private UIManager()
    {
        _uiModuleBases = new Dictionary<string, UIModuleBase>();
        uiModuleStack = new Stack<UIModuleBase>();
    }

    private void Awake()
    {
        tranCanvas = GameObject.Find("Canvas").transform;
    }

    public void ShowModule(string name,Object data=null)
    {
        if (!_uiModuleBases.ContainsKey(name))
        {
            GameObject module = ObjectPool.Instance.SpawnObj(name, tranCanvas ,data);
            UIModuleBase uiModuleBase = module.GetComponent<UIModuleBase>();
            _uiModuleBases.Add(name, uiModuleBase);
        }

        UIModuleBase moduleBase = _uiModuleBases[name];
        moduleBase.Show();

        if (moduleBase.moduleShowType == ModuleShowType.Single)
        {
            if (uiModuleStack.Count > 0)
            {
                UIModuleBase topModule = uiModuleStack.Peek();
                topModule.OnPause();
            }

            uiModuleStack.Push(moduleBase);
        }
    }

    public void HideModule(string name)
    {
        if (_uiModuleBases.ContainsKey(name))
        {
            _uiModuleBases[name].Hide();
        }
    }


    public void PopModule()
    {
        if (uiModuleStack.Count > 1)
        {
            UIModuleBase topModule = uiModuleStack.Pop();
            topModule.Hide();
            topModule = uiModuleStack.Peek();
            topModule.OnResume();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //最少留一个
            PopModule();
        }
    }
}