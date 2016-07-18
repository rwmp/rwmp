using System.Collections;
using JetBrains.Annotations;
using RWMP.Hook;
using UnityEngine;

namespace RWMP.Bootstrap
{
    [UsedImplicitly]
    public class BootstrapBehaviour : MonoBehaviour
    {
        [UsedImplicitly]
        public void Start()
        {
            StartCoroutine(Init());
        }

        private IEnumerator Init()
        {
            yield return new WaitForSeconds(0.5f);

            if (!HookSystem.IsInstalled)
            {
                HookSystem.Install();
            }
        }
    }
}