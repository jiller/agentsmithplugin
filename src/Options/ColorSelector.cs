using System;
using System.Drawing;
using System.Windows.Forms;

namespace AgentSmith.Options
{
    public partial class ColorSelector : UserControl
    {
        private Color _color;

        public Color Color
        {
            get { return _color; }
            set
            {
                button1.BackColor = value;         
                _color = value;
            }
        }

        public ColorSelector()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            button1.BackColor = _color;         
            base.OnLoad(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color = colorDialog.Color;
            }
        }
    }
}