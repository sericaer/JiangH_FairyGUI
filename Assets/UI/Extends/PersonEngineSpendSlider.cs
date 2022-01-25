using FairyGUI;
using FairyGUI.DataBind;
using FairyGUI.DataBind.BindCustomDatas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Extends
{
    class PersonEngineSpendSlider : GComponent
    {
        public GSlider slider;

        public GTextField title;

        public static void InitBindCustomData()
        {
            BindCustomData.dict.Add(typeof(PersonEngineSpendSlider), typeof(PersonEngineSpendSliderBind));
        }

        public override void ConstructFromResource()
        {
            base.ConstructFromResource();

            title = GetChild("title").asTextField;
        }

        public void SetTitle(string text)
        {
            title.text = text;
        }
    }

    [System.Serializable]
    internal class PersonEngineSpendSliderBind : BindCustomData
    {
        [System.Serializable]
        public new class BindTemplate : BindCustomData.BindTemplate
        {
            public string title;
        }

        public BindTemplate bind;
        public override IEnumerable<(string key, BindHandler handler)> BindUI2View(GObject gObject, INotifyPropertyChanged view)
        {
            var rslt = new List<(string key, BindHandler handler)>();
            if (bind == null)
            {
                return rslt;
            }

            BindEnable(bind.enable, view, gObject, rslt);

            BindTitle(gObject, view, rslt);

            return rslt;
        }

        private void BindTitle(GObject gObject, INotifyPropertyChanged view, List<(string key, BindHandler handler)> rslt)
        {
            var property = view.GetType().GetProperty(bind.title);
            if (property == null)
            {
                return;
            }

            var elem = gObject as PersonEngineSpendSlider;
            var handler = new BindHandler()
            {
                Init = (view) =>
                {
                    elem.title.text = property.GetValue(view).ToString();
                },
                OnViewUpdate = (view) =>
                {
                    elem.title.text = property.GetValue(view).ToString();
                }
            };

            rslt.Add((bind.title, handler));
        }
    }
}
