using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FiniteStateMachine
{
    public partial class Form1 : Form
    {
        float usableMoney = 0;
        State myState = State.IDLE;
        Input myInput;
        Output myOutput = null;
        List<Output> possibleOutputs = new List<Output>();
        Dictionary<KeyValuePair<State, Input>, KeyValuePair<State, Output>> keyValuePairs = new Dictionary<KeyValuePair<State, Input>, KeyValuePair<State, Output>>();
        bool excessMoney = false;

        public Form1()
        {
            InitializeComponent();
            possibleOutputs.Add(Gum.Instance);
            possibleOutputs.Add(Granola.Instance);
            possibleOutputs.Add(Quarter.Instance);
            quarterButton.Click += OnInputChanged;
            selectButton.Click += OnInputChanged;
            cancelButton.Click += OnInputChanged;
            excessButton.Click += OnInputChanged;
            InitializeKeyValuePairs();
            DisplayStatus();
        }

        private void InitializeKeyValuePairs()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    keyValuePairs.Add(new KeyValuePair<State, Input>((State)i, (Input)j), new KeyValuePair<State, Output>(State.IDLE, null));
                }
            }

            foreach (KeyValuePair<State, Input> kvp in keyValuePairs.Keys.ToList())
            {
                if (kvp.Value == Input.QUARTER)
                {
                    keyValuePairs.Remove(kvp);
                    keyValuePairs.Add(kvp, new KeyValuePair<State, Output>(State.EXPECTING_SELECTION, null));
                }
                else if (kvp.Value == Input.CANCEL && kvp.Key == State.EXPECTING_SELECTION || kvp.Value == Input.EXCESS)
                {
                    keyValuePairs.Remove(kvp);
                    keyValuePairs.Add(kvp, new KeyValuePair<State, Output>(State.IDLE, Quarter.Instance));
                }
            }
        }

        private void OnInputChanged(object sender, EventArgs e)
        {
            Control button = (Button)sender;
            string controlName = button.Name;
            controlName = WriteableToEnum(controlName);
            myInput = (Input)Enum.Parse(typeof(Input), controlName);
            KeyValuePair<State, Input> currentStateAndInput = new KeyValuePair<State, Input>(myState, myInput);

            if (myInput == Input.QUARTER)
            {
                usableMoney += 0.25f;
            }
            else if (myState == State.EXPECTING_SELECTION && myInput == Input.SELECT)
            {
                State dynamicState = myState;
                Output dynamicOutput = null;
                dynamicState = State.VENDING;
                dynamicOutput = possibleOutputs.FirstOrDefault(obj => obj.Name == CheckedRadioButtonName(vendingBox));

                if (dynamicOutput?.Price > usableMoney)
                {
                    dynamicState = State.EXPECTING_SELECTION;
                    dynamicOutput = null;
                }
                if (dynamicOutput?.Price < usableMoney)
                {
                    excessMoney = true;
                }

                keyValuePairs.Remove(currentStateAndInput);
                keyValuePairs.Add(currentStateAndInput, new KeyValuePair<State, Output>(dynamicState, dynamicOutput));
            }
            Quarter.Instance.Price = usableMoney;

            myState = keyValuePairs[currentStateAndInput].Key;
            myOutput = keyValuePairs[currentStateAndInput].Value;

            DisplayStatus();
        }

        private void DisplayStatus()
        {
            excessButton.Visible = false;
            selectButton.Enabled = true;
            cancelButton.Enabled = true;
            moneyLabel.Text = "Total: " + usableMoney.ToString("C");
            stateLabel.Text = "The vending machine is " + EnumToWriteable(myState.ToString()) + ".";
            if (myOutput == null)
            {
                outputLabel.Text = "";
                return;
            }

            outputLabel.Text = "You received ";

            if (myOutput == Quarter.Instance)
            {
                outputLabel.Text += usableMoney.ToString("C") + "!";
            }
            else
            {
                outputLabel.Text += EnumToWriteable(myOutput.Name.ToString()) + "!";
            }
            usableMoney -= myOutput.Price;
            moneyLabel.Text = "Total: " + usableMoney.ToString("C");

            if (excessMoney)
            {
                excessButton.Visible = true;
                selectButton.Enabled = false;
                cancelButton.Enabled = false;
                excessMoney = false;
            }
        }

        private string CheckedRadioButtonName(GroupBox groupBox)
        {
            string buttonAsString = "GUM";
            foreach (Control control in groupBox.Controls)
            {
                RadioButton radioButton = control as RadioButton;

                if (radioButton != null && radioButton.Checked)
                {
                    string selectedOption = radioButton.Name;
                    selectedOption = WriteableToEnum(selectedOption);
                    buttonAsString = selectedOption;
                }
            }
            return buttonAsString;
        }

        private string EnumToWriteable(string stringToChange)
        {
            stringToChange = stringToChange.ToLower();
            stringToChange = stringToChange.Replace('_', ' ');
            return stringToChange;
        }

        private string WriteableToEnum(string stringToChange)
        {
            stringToChange = stringToChange.Substring(0, stringToChange.Length - 6);
            stringToChange = stringToChange.ToUpper();
            return stringToChange;
        }

        enum State
        {
            IDLE,
            EXPECTING_SELECTION,
            VENDING
        }

        enum Input
        {
            QUARTER,
            SELECT,
            CANCEL,
            EXCESS
        }
    }
}
