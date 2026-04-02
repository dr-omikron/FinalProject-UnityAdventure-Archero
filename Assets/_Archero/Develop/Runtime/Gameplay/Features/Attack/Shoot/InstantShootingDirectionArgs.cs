using System.Collections.Generic;
using System.Linq;

namespace _Archero.Develop.Runtime.Gameplay.Features.Attack.Shoot
{
    public class InstantShootingDirectionArgs
    {
        private readonly List<InstantShotDirectionArgs> _args;

        public InstantShootingDirectionArgs(params InstantShotDirectionArgs[] args)
        {
            _args = new List<InstantShotDirectionArgs>(args);
        }

        public IReadOnlyList<InstantShotDirectionArgs> Args => _args;

        public void Add(InstantShotDirectionArgs shotDirectionArgs)
        {
            InstantShotDirectionArgs arg = _args.FirstOrDefault(ar => ar.Angle == shotDirectionArgs.Angle);

            if (arg != null)
            {
                arg.ProjectileCount += shotDirectionArgs.ProjectileCount;
                return;
            }

            _args.Add(shotDirectionArgs);
        }

        public void Remove(InstantShotDirectionArgs shotDirectionArgs)
        {
            InstantShotDirectionArgs arg = _args.FirstOrDefault(ar => ar.Angle == shotDirectionArgs.Angle);

            if (arg != null)
            {
                arg.ProjectileCount -= shotDirectionArgs.ProjectileCount;
                
                if (arg.ProjectileCount <= 0)
                    _args.Remove(shotDirectionArgs);
            }
        }
    }
}
