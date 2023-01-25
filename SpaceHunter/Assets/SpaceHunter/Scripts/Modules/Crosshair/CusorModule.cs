using UnityEngine;
namespace SpaceHunter.Scripts.Modules.Crosshair
{
    public class CusorModule
    {
        private bool _cursorVisible = false;
        
        private void DisableCursor()
        {
            _cursorVisible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void EnableCursor()
        {
            _cursorVisible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                if (_cursorVisible)
                {
                    EnableCursor();
                }
                else
                {
                    DisableCursor();
                }
            }
        }
    }
}