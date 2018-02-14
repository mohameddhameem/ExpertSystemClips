using Mommosoft.ExpertSystem;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertRecommendationSystem
{
    class HelperClass
    {
        public Mommosoft.ExpertSystem.Environment clipsEnvironment = new Mommosoft.ExpertSystem.Environment();
        public HelperClass(string path)
        {
            //Class initalizer
            ConsoleTraceListener tl = new ConsoleTraceListener();
            clipsEnvironment.AddRouter(new DebugRouter());
            clipsEnvironment.Load(path);
            clipsEnvironment.Reset();
        }
        /* call this method from the fron end. 
         * input = strButtonName = Next/Restart/Prev
         * initialFlag = call this for the first time
         * ChoiceOption = Yes / No
         */
        public void HandleRequest(string strButtonName, bool initialFlag, string ChoiceOption)
        {
            ReturnValueClass returnValue = new ReturnValueClass();
            String evalStr = "(find-all-facts ((?f state-list)) TRUE)";
            using (FactAddressValue f = (FactAddressValue)((MultifieldValue)clipsEnvironment.Eval(evalStr))[0])
            {
                string currentID = f.GetFactSlot("current").ToString();
                if (strButtonName.Equals("Next"))
                {
                    if (initialFlag)
                    {
                        clipsEnvironment.AssertString("(next " + currentID + ")");
                    }
                    else
                    {
                        clipsEnvironment.AssertString("(next " + currentID + " " + ChoiceOption + ")");
                    }
                    returnValue = getNextUIState();
                }
                else if (strButtonName.Equals("Restart"))
                {
                    clipsEnvironment.Reset();
                    returnValue = getNextUIState();
                }
                else if (strButtonName.Equals("Prev"))
                {
                    clipsEnvironment.AssertString("(prev " + currentID + ")");
                    returnValue = getNextUIState();
                }
            }
        }
        public ReturnValueClass getNextUIState()
        {
            //run the clips first
            clipsEnvironment.Run();
            ReturnValueClass returnValue = new ReturnValueClass();
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
                //get the state from clipse
                string state = evalFact.GetFactSlot("state").ToString();
                returnValue.State = state;
                using (MultifieldValue validAnswers = (MultifieldValue)evalFact.GetFactSlot("valid-answers"))
                {
                    for (int i = 0; i < validAnswers.Count; i++)
                    {
                        returnValue.validAnswers.Add((SymbolValue)validAnswers[i]);
                    }
                }
                returnValue.displayQuestion = GetString((SymbolValue)evalFact.GetFactSlot("display"));
            }
            return returnValue;
        }
        private string GetString(string name)
        {
            return ExpSysResources.ResourceManager.GetString(name);
        }
    }
    public class ReturnValueClass{
        public string State { get; set; } // state will be initial / final / middle (need to check this)
        public List<string> validAnswers { get; set; } // Yes / No
        public string displayQuestion { get; set; } // will be string from the Clipse
    }
}
