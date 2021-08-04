using System;
using UnityEngine;
using UnityEngine.UI;

namespace Frame.UI
{
    /// <summary>
    /// 管理常用脚本的脚本管理类，挂在重要的游戏对象上面
    /// </summary>
    public class UIWidget:MonoBehaviour
    {
        private Text text;
        private Image img;
        private Slider slider;
        private Animator anim;
        private CanvasGroup cg;
        private Button button;
        private Dropdown dropdown;
        private InputField inputField;
        private Toggle toggle;
        private ToggleGroup tg;

        public ToggleGroup Tg => tg;

        public Toggle Toggle => toggle;

        public InputField InputField => inputField;

        public Text Text => text;

        public Image Img => img;

        public Slider Slider => slider;

        public Animator Anim => anim;

        public CanvasGroup Cg => cg;

        public Button Button => button;

        public Dropdown Dropdown => dropdown;

        private void Awake()
        {
            text = GetComponent<Text>();
            img = GetComponent<Image>();
            slider = GetComponent<Slider>();
            anim = GetComponent<Animator>();
            cg = GetComponent<CanvasGroup>();
            button = GetComponent<Button>();
            dropdown = GetComponent<Dropdown>();
            inputField = GetComponent<InputField>();
            toggle = GetComponent<Toggle>();
            tg = GetComponent<ToggleGroup>();
        }
    }
}