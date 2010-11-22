using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;

namespace LETSI_WS_Stub_Client
{
    public partial class Form1 : Form
    {

        public int requestId = 0;
        private MultiAttempt req;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            req = new MultiAttempt();
            this.txtExternalRegId.Text = req.RegId;
            this.txtSecret.Text = req.Secret;
            this.txtURL.Text = req.URL;

            this.textBox1.Text = File.ReadAllText(@"README.txt");
        }



        private void btnTest_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "";
            req.RegId = this.txtExternalRegId.Text;
            req.Secret = this.txtSecret.Text;

            req.URL = this.txtURL.Text;
            req.output = this.textBox1;

            this.req.Log("starting tests");

            try
            {
                req.workflow();
                /*req.Log("******    starting 'GET' test");
                req.get();
                req.Log("******    starting 'togglePassFail' test");
                req.togglePassFail();
                req.Log("******    starting 'interactions' test");
                req.interactionsTest();*/

            }
            catch (Exception ex)
            {
                this.req.Log(ex.ToString());
            }
        }
    }
}