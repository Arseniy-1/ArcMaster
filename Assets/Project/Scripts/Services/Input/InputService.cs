using UnityEngine;

namespace Project.Scripts.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = nameof(Horizontal);
        protected const string Vertical = nameof(Vertical);
        private const string FireButton = "Fire";

        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonUp()
        {
            return SimpleInput.GetButtonUp(FireButton);
        }
        
        protected Vector2 GetSimpleInputAxis()
        {
            return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
        }
    }
}