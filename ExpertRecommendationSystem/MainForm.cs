using Mommosoft.ExpertSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpertRecommendationSystem
{
    public partial class MainForm : Form
    {
        private Mommosoft.ExpertSystem.Environment clipsEnvironment = new Mommosoft.ExpertSystem.Environment();
        public MainForm()
        {
            ConsoleTraceListener tl = new ConsoleTraceListener();
            InitializeComponent();
            openFileDialogCLP.DefaultExt =".clp";
            openFileDialogCLP.Filter = "Clips files (*.clp) | *.clp";
            DialogResult result = openFileDialogCLP.ShowDialog();
            string filePath = string.Empty;
            if (result == DialogResult.OK) // Test result.
            {
                filePath = openFileDialogCLP.FileName;
                MessageBox.Show("Load CLP successfull from :" + filePath);
            }
            clipsEnvironment.AddRouter(new DebugRouter());
            var path = @"ClipsScript.clp";
            clipsEnvironment.Load(filePath);
            clipsEnvironment.Reset();
        }
        private void OnClickButton(object sender, EventArgs e)
        {
            Button button = sender as Button;
            // Get the state-list.
            String evalStr = "(find-all-facts ((?f state-list)) TRUE)";
            using (FactAddressValue f = (FactAddressValue)((MultifieldValue)clipsEnvironment.Eval(evalStr))[0])
            {
                string currentID = f.GetFactSlot("current").ToString();

                if (button.Tag.Equals("Next"))
                {
                    if (GetCheckedChoiceButton() == null) { clipsEnvironment.AssertString("(next " + currentID + ")"); }
                    else
                    {
                        clipsEnvironment.AssertString("(next " + currentID + " " +
                                           (string)GetCheckedChoiceButton().Tag + ")");
                    }
                    NextUIState();
                }
                else if (button.Tag.Equals("Restart"))
                {
                    clipsEnvironment.Reset();
                    NextUIState();
                }
                else if (button.Tag.Equals("Prev"))
                {
                    clipsEnvironment.AssertString("(prev " + currentID + ")");
                    NextUIState();
                }
            }
        }
        private void NextUIState()
        {
            nextButton.Visible = false;
            prevButton.Visible = false;
            choicesPanel.Controls.Clear();
            clipsEnvironment.Run();

            // Get the state-list.
            String evalStr = "(find-all-facts ((?f state-list)) TRUE)";
            using (FactAddressValue allFacts = (FactAddressValue)((MultifieldValue)clipsEnvironment.Eval(evalStr))[0])
            {
                string currentID = allFacts.GetFactSlot("current").ToString();
                evalStr = "(find-all-facts ((?f UI-state)) " +
                               "(eq ?f:id " + currentID + "))";
            }

            using (FactAddressValue evalFact = (FactAddressValue)((MultifieldValue)clipsEnvironment.Eval(evalStr))[0])
            {
                string state = evalFact.GetFactSlot("state").ToString();
                if (state.Equals("initial"))
                {
                    nextButton.Visible = true;
                    nextButton.Tag = "Next";
                    nextButton.Text = "Next";
                    prevButton.Visible = false;
                }
                else if (state.Equals("final"))
                {
                    nextButton.Visible = true;
                    nextButton.Tag = "Restart";
                    nextButton.Text = "Restart";
                    prevButton.Visible = false;
                }
                else
                {
                    nextButton.Visible = true;
                    nextButton.Tag = "Next";
                    prevButton.Tag = "Prev";
                    prevButton.Visible = true;
                }



                using (MultifieldValue validAnswers = (MultifieldValue)evalFact.GetFactSlot("valid-answers"))
                {
                    //clear of the old label
                    lblAnsClips.Text = string.Empty;
                    String selected = evalFact.GetFactSlot("response").ToString();
                    for (int i = 0; i < validAnswers.Count; i++)
                    {
                        RadioButton rb = new RadioButton();
                        rb.Text = (SymbolValue)validAnswers[i];
                        rb.Tag = rb.Text;
                        lblAnsClips.Text = lblAnsClips.Text + " "+ rb.Text;
                        rb.Visible = true;
                        rb.Location = new Point(10, 20 * (i + 1));
                        choicesPanel.Controls.Add(rb);
                    }
                    lblAnsClips.Text = lblAnsClips.Text + " :Updated on " + DateTime.Now.ToLongTimeString();
                }
                messageLabel.Text = GetString((SymbolValue)evalFact.GetFactSlot("display"));
            }
        }
        private void ShowChoices(bool visible)
        {
            foreach (Control control in choicesPanel.Controls)
            {
                control.Visible = visible;
            }
        }
        private RadioButton GetCheckedChoiceButton()
        {
            foreach (RadioButton control in choicesPanel.Controls)
            {
                if (control.Checked)
                {
                    return control;
                }
            }
            return null;
        }
        private string GetString(string name)
        {            
            return ExpSysResources.ResourceManager.GetString(name);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            NextUIState();
        }        
    }
}
