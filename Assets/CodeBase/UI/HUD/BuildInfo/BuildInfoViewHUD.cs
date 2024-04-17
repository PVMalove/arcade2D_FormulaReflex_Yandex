using CodeBase.UI.HUD.Base;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.HUD.BuildInfo
{
    public class BuildInfoViewHUD : HUDBase<IBuildInfoPresenter>
    {
        [SerializeField] private Text textBuildNumber;

        protected override void Initialize(IBuildInfoPresenter presenter)
        {
            base.Initialize(presenter);
            FillData(presenter);
        }

        private void FillData(IBuildInfoPresenter config)
        {
            textBuildNumber.text = config.BuildNumber;
        }
    }
}