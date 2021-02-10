using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Color = System.Drawing.Color;

namespace Epicture.Services
{
    public class Exeption
    {
        public Exeption()
        {
        }

        public void Error(ref Label label, string msg)
        {
            label.TextColor = Color.Red;
            label.Text = msg;
        }
        public void Inform(ref Label label, string msg)
        {
            label.TextColor = Color.Green;
            label.Text = msg;
        }
        public void Clear(ref Label label, string msg)
        {
            label.TextColor = Color.White;
            label.Text = msg;
        }
    }
}