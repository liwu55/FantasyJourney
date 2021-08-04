using UnityEngine;

namespace Frame.Utility
{
    public class ResManager:SingleTonObj<ResManager>
    {
        private ResManager()
        {
        }

        public static Sprite LoadImg(string name)
        {
            string path = ConfigurationManager.Instance.GetPathByName(name);
            return Resources.Load<Sprite>(path);
        }
    }
}