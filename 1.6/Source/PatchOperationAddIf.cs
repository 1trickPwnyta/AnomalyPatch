using HarmonyLib;
using System.Xml;
using Verse;

namespace AnomalyPatch
{
    public class PatchOperationAddIf : PatchOperationAdd
    {
        private XmlContainer setting;

        protected override bool ApplyWorker(XmlDocument xml)
        {
            string settingText = setting.node.InnerText;
            return (bool)typeof(AnomalyPatchSettings).Field(settingText).GetValue(null) ? base.ApplyWorker(xml) : true;
        }
    }
}
