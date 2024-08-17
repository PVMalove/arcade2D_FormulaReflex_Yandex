using UnityEngine;
using System.IO;

namespace YG.EditorScr.BuildModify
{
    public partial class ModifyBuildManager
    {
        public static void Payments()
        {
            string initFunction = "paymentsData = await GetPayments();\nconsole.log('[INDEX] -> Initialization Payments SDK');";
            AddIndexCode(initFunction, CodeType.init);

            string donorPatch = Application.dataPath + "/Plugins/YandexGame/ScriptsYG/Payments/Editor/Payments_js.js";
            string donorText = File.ReadAllText(donorPatch);

            AddIndexCode(donorText, CodeType.js);
        }
    }
}
