using firstParser.Core;
using firstParser.Core.Habra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace firstParser
{
    public partial class Form1 : Form
    {
        ParserWorker<string[]> parserMP;
        ParserWorker<string[]> ParserUniverList;
        ParserWorker<string[]> parserUniverInfo;
        public Form1()
        {
            InitializeComponent();
            parserMP = new ParserWorker<string[]>(
                new MainPageParser()
                );
            parserMP.OnComplited += Parser_OnCompleted;
            parserMP.OnNewData += Parser_OnNewDataMP;
            
        }

        private void Parser_OnNewDataMP(object arg1, string[] arg2)
        {
            List<string> CityList = new List<string>();
            CityList.AddRange(arg2);
            ParserUniverList = new ParserWorker<string[]>(
                new UniverListParser(),
                new UniverListSettings(CityList[12])
                );
            ParserUniverList.OnNewData += Parser_OnNewDataUniverLisr;
            ParserUniverList.Start();
            //ListTitles.Items.Add(arg2[12]);
        }

        private void Parser_OnNewDataUniverLisr(object arg1, string[] arg2)
        {
            //ListTitles.Items.AddRange(arg2);
            List<string> univerLink = new List<string>();
            univerLink.AddRange(arg2);
            parserUniverInfo = new ParserWorker<string[]>(
                new UniverParser(),
                new UniverSettings(univerLink[0])
                );
            parserUniverInfo.OnNewData += ParserOnNewDataUniver;
            parserUniverInfo.Start();
;        }
        private void ParserOnNewDataUniver(object arg1, string[] arg2)
        {
            ListTitles.Items.AddRange(arg2);
        }


        private void Parser_OnCompleted(object obj)
        {
            MessageBox.Show("Compleat");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            parserMP.ParserSettings = new MainPageSettings();
            parserMP.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            parserMP.Abort();
        }
    }
}
