using _Archero.Develop.Runtime.Gameplay.EntitiesCore;
using _Archero.Develop.Runtime.Utilities;
using UnityEngine;

namespace _Archero.Develop.Runtime.Gameplay.Features.Sensors
{
    public class BodyCollider : IEntityComponent
    {
        public CapsuleCollider Value;
    }

    public class ContactsDetectingMask : IEntityComponent
    {
        public LayerMask Value;
    }

    public class ContactsCollidersBuffer : IEntityComponent
    {
        public Buffer<Collider> Value;
    }

    public class ContactsEntitiesBuffer : IEntityComponent
    {
        public Buffer<Entity> Value;
    }
}
