namespace _Archero.Develop.Runtime.Gameplay.EntitiesCore
{
	public partial class Entity
	{
		public _Archero.Develop.Runtime.Gameplay.Features.Sensors.BodyCollider BodyColliderC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.Sensors.BodyCollider>();

		public UnityEngine.CapsuleCollider BodyCollider => BodyColliderC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddBodyCollider(UnityEngine.CapsuleCollider value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.Sensors.BodyCollider {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.Sensors.ContactsDetectingMask ContactsDetectingMaskC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.Sensors.ContactsDetectingMask>();

		public UnityEngine.LayerMask ContactsDetectingMask => ContactsDetectingMaskC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddContactsDetectingMask(UnityEngine.LayerMask value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.Sensors.ContactsDetectingMask {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.Sensors.ContactsCollidersBuffer ContactsCollidersBufferC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.Sensors.ContactsCollidersBuffer>();

		public _Archero.Develop.Runtime.Utilities.Buffer<UnityEngine.Collider> ContactsCollidersBuffer => ContactsCollidersBufferC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddContactsCollidersBuffer(_Archero.Develop.Runtime.Utilities.Buffer<UnityEngine.Collider> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.Sensors.ContactsCollidersBuffer {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.Sensors.ContactsEntitiesBuffer ContactsEntitiesBufferC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.Sensors.ContactsEntitiesBuffer>();

		public _Archero.Develop.Runtime.Utilities.Buffer<_Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity> ContactsEntitiesBuffer => ContactsEntitiesBufferC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddContactsEntitiesBuffer(_Archero.Develop.Runtime.Utilities.Buffer<_Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.Sensors.ContactsEntitiesBuffer {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.MoveDirection MoveDirectionC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.MovementFeature.MoveDirection>();

		public _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> MoveDirection => MoveDirectionC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMoveDirection()
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.MoveDirection { Value = new _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3>() });
		}

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMoveDirection(_Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.MoveDirection {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.MoveSpeed MoveSpeedC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.MovementFeature.MoveSpeed>();

		public _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> MoveSpeed => MoveSpeedC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMoveSpeed()
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.MoveSpeed { Value = new _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() });
		}

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMoveSpeed(_Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.MoveSpeed {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.CanMove CanMoveC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.MovementFeature.CanMove>();

		public _Archero.Develop.Runtime.Utilities.Conditions.ICompositeCondition CanMove => CanMoveC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCanMove(_Archero.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.CanMove {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.RotationDirection RotationDirectionC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.MovementFeature.RotationDirection>();

		public _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> RotationDirection => RotationDirectionC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddRotationDirection()
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.RotationDirection { Value = new _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3>() });
		}

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddRotationDirection(_Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.RotationDirection {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.RotationSpeed RotationSpeedC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.MovementFeature.RotationSpeed>();

		public _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> RotationSpeed => RotationSpeedC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddRotationSpeed()
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.RotationSpeed { Value = new _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() });
		}

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddRotationSpeed(_Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.RotationSpeed {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.CanRotate CanRotateC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.MovementFeature.CanRotate>();

		public _Archero.Develop.Runtime.Utilities.Conditions.ICompositeCondition CanRotate => CanRotateC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCanRotate(_Archero.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.MovementFeature.CanRotate {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.CurrentHealth CurrentHealthC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.LifeCycle.CurrentHealth>();

		public _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> CurrentHealth => CurrentHealthC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCurrentHealth()
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.CurrentHealth { Value = new _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() });
		}

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCurrentHealth(_Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.CurrentHealth {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.MaxHealth MaxHealthC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.LifeCycle.MaxHealth>();

		public _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> MaxHealth => MaxHealthC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMaxHealth()
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.MaxHealth { Value = new _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() });
		}

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMaxHealth(_Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.MaxHealth {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.IsDead IsDeadC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.LifeCycle.IsDead>();

		public _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> IsDead => IsDeadC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddIsDead()
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.IsDead { Value = new _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>() });
		}

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddIsDead(_Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.IsDead {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.MustDie MustDieC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.LifeCycle.MustDie>();

		public _Archero.Develop.Runtime.Utilities.Conditions.ICompositeCondition MustDie => MustDieC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMustDie(_Archero.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.MustDie {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.MustSelfRelease MustSelfReleaseC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.LifeCycle.MustSelfRelease>();

		public _Archero.Develop.Runtime.Utilities.Conditions.ICompositeCondition MustSelfRelease => MustSelfReleaseC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddMustSelfRelease(_Archero.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.MustSelfRelease {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessInitialTime DeathProcessInitialTimeC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessInitialTime>();

		public _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> DeathProcessInitialTime => DeathProcessInitialTimeC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddDeathProcessInitialTime()
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessInitialTime { Value = new _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() });
		}

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddDeathProcessInitialTime(_Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessInitialTime {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessCurrentTime DeathProcessCurrentTimeC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessCurrentTime>();

		public _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> DeathProcessCurrentTime => DeathProcessCurrentTimeC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddDeathProcessCurrentTime()
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessCurrentTime { Value = new _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() });
		}

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddDeathProcessCurrentTime(_Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.DeathProcessCurrentTime {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.InDeathProcess InDeathProcessC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.LifeCycle.InDeathProcess>();

		public _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> InDeathProcess => InDeathProcessC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddInDeathProcess()
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.InDeathProcess { Value = new _Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean>() });
		}

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddInDeathProcess(_Archero.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Boolean> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.LifeCycle.InDeathProcess {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageRequest TakeDamageRequestC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageRequest>();

		public _Archero.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single> TakeDamageRequest => TakeDamageRequestC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTakeDamageRequest()
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageRequest { Value = new _Archero.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single>() });
		}

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTakeDamageRequest(_Archero.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageRequest {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageEvent TakeDamageEventC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageEvent>();

		public _Archero.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single> TakeDamageEvent => TakeDamageEventC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTakeDamageEvent()
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageEvent { Value = new _Archero.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single>() });
		}

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddTakeDamageEvent(_Archero.Develop.Runtime.Utilities.Reactive.ReactiveEvent<System.Single> value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.ApplyDamage.TakeDamageEvent {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Features.ApplyDamage.CanApplyDamage CanApplyDamageC => GetComponent<_Archero.Develop.Runtime.Gameplay.Features.ApplyDamage.CanApplyDamage>();

		public _Archero.Develop.Runtime.Utilities.Conditions.ICompositeCondition CanApplyDamage => CanApplyDamageC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddCanApplyDamage(_Archero.Develop.Runtime.Utilities.Conditions.ICompositeCondition value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Features.ApplyDamage.CanApplyDamage {Value = value});
		}

		public _Archero.Develop.Runtime.Gameplay.Common.RigidbodyComponent RigidbodyC => GetComponent<_Archero.Develop.Runtime.Gameplay.Common.RigidbodyComponent>();

		public UnityEngine.Rigidbody Rigidbody => RigidbodyC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddRigidbody(UnityEngine.Rigidbody value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Common.RigidbodyComponent {Value = value});
		}

	}
}
