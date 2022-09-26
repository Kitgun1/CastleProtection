using System;
using Entity.Movement;
using UnityEngine;

namespace Entity.Character
{
    public class CharacterUnit : Entity, IEntity
    {
        public PhysicsMovementData data;

        private Rigidbody _rigidbody;
        private PhysicsMovement _physicsMovement;
        private CharacterMovement _characterMovement;

        protected void Awake()
        {
            _rigidbody = gameObject.AddComponent<Rigidbody>();

            _rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            
            _physicsMovement = new PhysicsMovement(_rigidbody, data);
            _characterMovement = gameObject.AddComponent<CharacterMovement>();
            _characterMovement._physicsMovement = _physicsMovement;

        }
    }
}