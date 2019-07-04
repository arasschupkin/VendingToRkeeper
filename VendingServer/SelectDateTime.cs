using System;
using System.Windows.Forms;

namespace Vending
{
    public partial class SelectDateTime : Form
    {
        public DateTime BeginDate;
        public DateTime EndDate;
        public SelectDateTime()
        {
            InitializeComponent();
            dtBeginDate.Value = DateTime.Now.Date.AddDays(-DateTime.Now.Date.Day + 1);            
            dtEndDate.Value = DateTime.Now.Date.AddMonths(1).AddDays(-DateTime.Now.Date.Day + 1).AddHours(23).AddMinutes(59).AddSeconds(59);

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            BeginDate = dtBeginDate.Value;
            EndDate = dtEndDate.Value;
        }

        private void SelectDateTime_Shown(object sender, EventArgs e)
        {
            dtBeginDate.Focus();
        }
    }
}
