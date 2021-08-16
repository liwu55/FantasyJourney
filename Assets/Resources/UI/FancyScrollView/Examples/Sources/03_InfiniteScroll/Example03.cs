/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using Game.bean;
using System.Linq;
using UnityEngine;

namespace FancyScrollView.Example03
{
    class Example03 : MonoBehaviour
    {
        [SerializeField] ScrollView scrollView = default;

        void Start()
        {
            var items = Enumerable.Range(0,CellNmb())
                .Select(i => new ItemData(CellMsage(i)))
                .ToArray();

            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
        }
        private int CellNmb()
        {
            return MapsInfo.Instance.getMapLength();
        }
        private string CellMsage(int i)
        {
            
            return MapsInfo.Instance.getMapName(i);
        }
    }
}
