using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BacklogKiller.Controls
{
    public partial class SnackBar : UserControl
    {
        public SnackBar()
        {
            InitializeComponent();
            Hide();
        }

        private void SnackBar_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        public void Show(string message)
        {
            Show(message, Duration.Short);
        }

        public void Show(string message, Duration duration)
        {
            const int MARGIN = 10;

            lblMessage.Text = message;

            this.Width = MARGIN + lblMessage.Width + MARGIN;
            this.Height = MARGIN + lblMessage.Height + MARGIN;

            lblMessage.Left = MARGIN;
            lblMessage.Top = MARGIN;

            this.Left = (this.ParentForm.Width / 2) - (this.Width / 2);
            this.Top = this.ParentForm.Height - (this.Height * 3);

            base.Show();

            var milliseconds = duration == Duration.Short ? 1500 : 2750;
            Task.Delay(milliseconds).ContinueWith(task => base.Hide());
        }
    }
}
