using UnityEngine;
using Verse;

namespace RWMP.Bootstrap
{
    public class Bootstrap : ITab
    {
        private GameObject _gameObject;

        public Bootstrap()
        {
            LongEventHandler.ExecuteWhenFinished(Init);
        }

        private void Init()
        {
            _gameObject = new GameObject("RWMP");
            _gameObject.AddComponent<BootstrapBehaviour>();
            Object.DontDestroyOnLoad(_gameObject);
        }

        protected override void FillTab()
        {
        }
    }
}