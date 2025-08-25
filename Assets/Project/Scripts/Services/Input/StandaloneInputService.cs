using UnityEngine;

namespace Project.Scripts.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = GetSimpleInputAxis();

                if (axis == Vector2.zero)
                    axis = GetUnityAxis();
                
                return axis;
            }
        }

        private Vector2 GetUnityAxis()
        {
            return new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
        }
    }
}