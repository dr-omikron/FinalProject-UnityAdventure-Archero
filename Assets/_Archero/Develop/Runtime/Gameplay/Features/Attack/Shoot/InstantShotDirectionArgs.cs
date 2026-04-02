using System;

namespace _Archero.Develop.Runtime.Gameplay.Features.Attack.Shoot
{
    public class InstantShotDirectionArgs
    {
        private int _angle;
        private int _projectileCount;

        public InstantShotDirectionArgs(int angle, int projectileCount)
        {
            _angle = angle;
            _projectileCount = projectileCount;
        }

        public int Angle => _angle;

        public int ProjectileCount
        {
            get => _projectileCount;

            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value));

                _projectileCount = value;
            }
        }
    }
}
