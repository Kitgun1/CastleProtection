using System;
using UnityEngine;

namespace Entity.Movement
{
    public class PhysicsMovement
    {
	    private readonly Rigidbody _rigidbody;
	    private readonly PhysicsMovementData _data;

    #region Conctructor

	    public PhysicsMovement(Rigidbody rigidbody, PhysicsMovementData data)
	    {
		    if (rigidbody == null) throw new NullReferenceException($"{nameof(_rigidbody)} is null!");
		    _rigidbody = rigidbody;
		    _data = data;
	    }

    #endregion

    #region Hash Fields

	    private bool _grounded;
	    private Vector3 _velocity = Vector3.zero;

    #endregion

    #region Public Property

	    public float Velocity => _velocity.magnitude;

    #endregion
	    
	#region Public Methods

		public void Move(Vector2 direction)
		{
			Grounded();

			if (!_grounded && !_data.airControl) return;
			
			// Move the character
			Vector2 newDirection = direction * (10f * _data.speed * Time.fixedDeltaTime);
			Vector3 targetVelocity = new Vector3(newDirection.x, _rigidbody.velocity.y, newDirection.y);

			_rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, targetVelocity, ref _velocity, _data.movementSmoothing);
		}

		public void Jump()
		{
			if (!_grounded) return;
        
			// Add a vertical force to the player.
			_grounded = false;
			_rigidbody.AddForce(new Vector3(0f, _data.jumpForce), ForceMode.Impulse);
		}

	#endregion

	#region Private Methods

		private void Grounded()
		{
			bool wasGrounded = _grounded;
			_grounded = false;
        	
			var colliders = Physics.OverlapBox(_data.groundCheck.position, _data.halfExtents, Quaternion.identity, _data.whatIsGround);
			foreach (var coll in colliders) _grounded = true;
		}

	#endregion
		
    }
    
    [Serializable]
    public struct PhysicsMovementData
    {
	    [Header("Movement options:")]
	    public float speed;								//40f;
	    [Range(0, 10f)] public float jumpForce;			//2f;
	    [Range(0, .3f)] public float movementSmoothing; //0.05f;
	    public bool airControl;							//false;
	    
	    [Header("Collision options:")]
	    public Transform groundCheck;
	    public Vector3 halfExtents;						//Vector2(0.2f, 0.2f);
	    
	    [Header("Sort options:")]
	    public LayerMask whatIsGround;
    }
}