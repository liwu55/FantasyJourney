using Photon.Pun;

namespace Frame.SingleTon
{
    public class SingleTonMonoPhoton<T> : MonoBehaviourPun where T : class
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                return instance;
            }
        }

        protected virtual void Awake()
        {
            instance = this as T;
        }
    }
}