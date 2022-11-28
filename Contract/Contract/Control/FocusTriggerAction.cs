using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Contract.Control
{
    public class FocusTriggerAction : TriggerAction<CustomEditor>
    {
        public bool Focused { get; set; }

        protected override async void Invoke(CustomEditor editor)
        {
            await Task.Delay(1000);

            if (Focused)
            {
                editor.Focus();
            } 
        }
    }
}
