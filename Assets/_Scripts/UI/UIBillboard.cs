using UnityEngine;

namespace UI
{

    // This namespace is used for UI elements, but we don't need to use it in this script.

    public class UIBillboard : MonoBehaviour
    {

        private Camera mainCamera;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            if (mainCamera != null)
            {
                gameObject.transform.LookAt(mainCamera.transform.position,mainCamera.transform.up);
            }
        }
    }
}
