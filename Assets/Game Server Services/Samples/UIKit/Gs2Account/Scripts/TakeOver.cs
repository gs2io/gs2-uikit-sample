using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gs2.Unity.UiKit.Sample.Gs2Account
{
    public class TakeOver : MonoBehaviour
    {
        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}