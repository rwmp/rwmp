using System.Collections;
using RWMP.Hook;
using UnityEngine;

namespace RWMP.Bootstrap
{
    public class BootstrapBehaviour : MonoBehaviour
    {
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