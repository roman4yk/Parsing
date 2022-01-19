using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            UpdateWebBrowser uwb = new UpdateWebBrowser();
            Parser parser = new Parser();
            parser.GetValutesFromDOM();
            parser.eventFinishLoad += FillDataGridTeams;
            dataGridView1.AutoGenerateColumns=true;
            dataGridView2.AutoGenerateColumns = true;
            ParserPlayers ps = new ParserPlayers();
            var source = new BindingSource();
            source.DataSource = ps.GetPlayers();
            dataGridView2.DataSource = source;
        }
        public void FillDataGridTeams(List<Team> teams)
        {
            var source = new BindingSource();
            source.DataSource = teams;
            dataGridView1.DataSource = source;
        }
    }
}
