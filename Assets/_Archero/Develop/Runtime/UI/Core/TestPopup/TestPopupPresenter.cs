using _Archero.Develop.Runtime.Utilities.CoroutinesManagement;

namespace _Archero.Develop.Runtime.UI.Core.TestPopup
{
    public class TestPopupPresenter : PopupPresenterBase
    {
        private readonly TestPopupView _view;

        public TestPopupPresenter(TestPopupView view, ICoroutinesPerformer coroutinesPerformer) : base(coroutinesPerformer)
        {
            _view = view;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();
            _view.SetText("TEST TITLE");
        }
    }
}
