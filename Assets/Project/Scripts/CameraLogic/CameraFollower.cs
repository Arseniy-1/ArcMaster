using UnityEngine;

namespace Project.Scripts.CameraLogic
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        
        public float RotationAngleX;
        public int Distance;
        public float OffsetY;

        private void LateUpdate()
        {
            if(_target == null)
                return;
            
            Quaternion rotation = Quaternion.Euler(RotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -Distance) + FollowingPointPosition();
            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject following)
        {
            _target = following.transform;
        }
        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _target.position;
            followingPosition.y += OffsetY;
            return followingPosition;
        }
    }
}