using System;
using Project.Scripts.Infrastructure;
using Project.Scripts.Services;
using Project.Scripts.Services.Input;
using UnityEngine;

namespace Project.Scripts.Player
{
    public class PlayerMover : MonoBehaviour
    { 
        private IInputService _inputService;
        private Camera _camera;
        
        public CharacterController CharacterController;
        public float MovementSpeed;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            Vector3 movementDirection = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementDirection = _camera.transform.TransformDirection(_inputService.Axis);
                movementDirection.y = 0;
                movementDirection.Normalize();
                
                transform.forward = movementDirection;
            } 
            
            // movementDirection += Physics.gravity;
            
            CharacterController.Move(movementDirection * Time.deltaTime * MovementSpeed);    
        }
    }
}