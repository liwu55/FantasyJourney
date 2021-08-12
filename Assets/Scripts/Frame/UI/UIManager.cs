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

    public T GetModule<T>(string ModuleName) where T:UIModuleBase
    {
        if (_uiModuleBases.ContainsKey(ModuleName))
        {
            return _uiModuleBases[ModuleName] as T;
        }

        return null;
        
    }
    
    
    public void ShowModule(string name,Object data=null)
    {
        EnsureCanvasExist();
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

    private void EnsureCanvasExist()
    {
        //转换场景后需要重新获取新场景的Canvas
        //先找一下
        if (tranCanvas == null)
        {
            var goCanvas = GameObject.Find("Canvas");
            if (goCanvas != null)
            {
                tranCanvas = goCanvas.transform;
            }
        }
        //找不到新建一个
        if (tranCanvas == null)
        {
            tranCanvas = ObjectPool.Instance.SpawnObj("Canvas").transform;
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