using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mega_World_Mall_Linking.Views
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
            Progress.Minimum = 0;
            Progress.AnimationStep = 1;
        }

        private void Loading_Load(object sender, EventArgs e)
        {

        }

        public void SetProgress(int value, int max, string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetProgress(value, max, message)));
                return;
            }

            Progress.Maximum = max;
            Progress.Value = Math.Min(value, max);
            lbl_date.Text = message;
        }
    }
}
