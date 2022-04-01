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
           // ParserGames pG = new ParserGames();
            //pG.GetGames();
           // ParserTeams pT = new ParserTeams();
            UpdateWebBrowser uwb = new UpdateWebBrowser();
            //Parser parserT = new Parser();

            //parserT.GetValutesFromDOM();
            //parserT.eventFinishLoad += InitTeams;

            dataGridView1.AutoGenerateColumns=true;
            dataGridView2.AutoGenerateColumns = true;

            ParserPlayers parserP = new ParserPlayers();
            InitPlayers(parserP.GetPlayers());
        }

        public void InitTeams(List<Team> teams)
        {
            DataModel.ListTeams=teams;
            var source = new BindingSource();
            source.DataSource = teams;
            dataGridView1.DataSource = source;
        }
        public void InitPlayers(List<Player> players)
        {
            DataModel.ListPlayers=players;
            var source = new BindingSource();
            source.DataSource = DataModel.ListPlayers;
            dataGridView2.DataSource = source;
        }
    }
}
