using TMPro;
using UnityEngine;

namespace ADMOB
{
    public class GetBundleVersion : MonoBehaviour
    {
        private void OnEnable(){
            GetComponent<TextMeshProUGUI>().text = Application.version;
        }
    }
}
