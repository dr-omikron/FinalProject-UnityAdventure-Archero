using DG.Tweening;

namespace _Archero.Develop.Runtime.UI.Core
{
    public interface IShowableView : IView
    {
        Tween Show();
        Tween Hide();
    }
}
