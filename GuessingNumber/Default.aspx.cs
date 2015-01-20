using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GuessingNumber.Model;

namespace GuessingNumber
{
    public partial class Default : System.Web.UI.Page
    {
        //Sessionsvariabel med referens till affärslogikobjektet
        private SecretNumber secretNumber
        {
            get { return Session["secretNumber"] as SecretNumber; }
            set { Session["secretNumber"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void GuessButton_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                if (secretNumber == null)
                {
                    //Instansierar ett nytt affärslogikobjekt
                    secretNumber = new SecretNumber();
                    secretNumber.Initialize();
                }

                //Gör om inmatning i textfältet till heltal
                int guess = int.Parse(GuessedNumber.Text);

                try
                {
                    //Anropar metoden MakeGuess
                    Outcome outcome = secretNumber.MakeGuess(guess);

                    //Gör knappar och fält tillgängliga/otillgängliga beroende på om det går att göra en gissning
                    if (secretNumber.CanMakeGuess == true)
                    {
                        //Sätter fokus på textfältet och markerar texten i textfältet
                        //(http://stackoverflow.com/questions/951435/highlight-text-in-net-select-isnt-available)
                        SetFocus(GuessedNumber);
                        GuessedNumber.Attributes.Add("onfocus", "this.select();");
                    }
                    else
                    {
                        GuessedNumber.Enabled = false;
                        GuessButton.Enabled = false;
                        NewNoButton.Visible = true;
                        SetFocus(NewNoButton);
                    }

                    //Skriver ut de gissningar som har gjorts
                    foreach (int prevguess in secretNumber.PreviousGuesses)
                    {
                        GuessListBox.Items.Add(new ListItem(prevguess.ToString(), prevguess.ToString()));
                    }

                    //Skriver ut texten i GuessLabel beroende på resultatet av gissningen
                    if (outcome == Outcome.Correct)
                    {
                        GuessLabel.Text = " Grattis! Du klarade det på " + secretNumber.Count.ToString() + " försök.";
                    }
                    if (outcome == Outcome.High)
                    {
                        GuessLabel.Text = " För högt";
                    }
                    if (outcome == Outcome.Low)
                    {
                        GuessLabel.Text = " För lågt";
                    }
                    if (outcome == Outcome.PreviousGuess)
                    {
                        GuessLabel.Text = " Du har redan gissat på talet.";
                    }
                    if (outcome == Outcome.NoMoreGuesses)
                    {
                        GuessLabel.Text = " Du har inga gissningar kvar. Det hemliga talet var " + secretNumber.Number.ToString() + ".";
                    }

                    GuessPlaceHolder.Visible = true;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    foreach (int prevguess in secretNumber.PreviousGuesses)
                    {
                        GuessListBox.Items.Add(new ListItem(prevguess.ToString(), prevguess.ToString()));
                    }
                    GuessLabel.Text = ex.ParamName;
                    GuessPlaceHolder.Visible = true;
                }
            }
        }

        protected void NewNoButton_Click(object sender, EventArgs e)
        {
            //Initierar objektet med ett nytt slumptal genom att anropa metoden Initialize
            secretNumber.Initialize();
        }
    }
}
