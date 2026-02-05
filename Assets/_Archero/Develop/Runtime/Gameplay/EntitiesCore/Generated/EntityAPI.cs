namespace _Archero.Develop.Runtime.Gameplay.EntitiesCore
{
	public partial class Entity
	{
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

		public _Archero.Develop.Runtime.Gameplay.Common.RigidbodyComponent RigidbodyC => GetComponent<_Archero.Develop.Runtime.Gameplay.Common.RigidbodyComponent>();

		public UnityEngine.Rigidbody Rigidbody => RigidbodyC.Value;

		public _Archero.Develop.Runtime.Gameplay.EntitiesCore.Entity AddRigidbody(UnityEngine.Rigidbody value)
		{
			return AddComponent(new _Archero.Develop.Runtime.Gameplay.Common.RigidbodyComponent {Value = value});
		}

	}
}
