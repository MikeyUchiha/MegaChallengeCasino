using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MegaChallengeCasino
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                string[] reels = new string[] { generateRandomImage(), generateRandomImage(), generateRandomImage() };
                displayImages(reels);
                ViewState.Add("Money", 100);
                outputAmountOfMoney();
            }
        }

        protected void pullLeverButton_Click(object sender, EventArgs e)
        {
            double bet = validateBet();
            double outcome = pullLever();
            outputMessage(bet, outcome);
            changeAmountOfMoney(bet, outcome);
            outputAmountOfMoney();
        }

        Random random = new Random();

        private string generateRandomImage()
        {
            string[] images = new string[] { "Strawberry", "Bar", "Lemon", "Bell", "Clover", 
                "Cherry", "Diamond", "Orange", "Seven", "HorseShoe", "Plum", "Watermelon" };
            return images[random.Next(11)];
        }

        private void displayImages(string[] reels)
        {
            reelOneImage.ImageUrl = "~/Images/" + reels[0] + ".png";
            reelTwoImage.ImageUrl = "~/Images/" + reels[1] + ".png";
            reelThreeImage.ImageUrl = "~/Images/" + reels[2] + ".png";
        }


        private double pullLever()
        {
            string[] reels = new string[3] { generateRandomImage(), generateRandomImage(), generateRandomImage() };
            displayImages(reels);

            double bet = validateBet();
            int multiplier = valueOfPull(reels);
            return bet * multiplier;
        }


        private double validateBet()
        {
            double bet = 0;
            if (!double.TryParse(betTextBox.Text, out bet)) return bet;
            return bet;
        }

        private int valueOfPull(string[] reels)
        {
            if (hasBar(reels)) return 0;
            if (isJackPot(reels)) return 100;

            int multiplier = 0;
            if (hasCherries(reels, out multiplier)) return multiplier;

            return 0;
        }

        private bool hasBar(string[] reels)
        {
            for(int i = 0; i < reels.Length; i++)
            {
                if (reels[i] == "Bar") return true;
            }
            return false;
        }

        private bool isJackPot(string[] reels)
        {
            if (reels[0] == "Seven" & reels[1] == "Seven" & reels[2] == "Seven")
                return true;
            return false;
        }

        private bool hasCherries(string[] reels, out int multiplier)
        {
            multiplier = calculateMultiplier(reels);
            if (multiplier > 0) return true;
            return false;
        }

        private int countCherries(string[] reels)
        {
            int cherryCount = 0;
            for (int i = 0; i < reels.Length; i++)
            {
                if (reels[i] == "Cherry") cherryCount++;
            }
            return cherryCount;
        }
        private int calculateMultiplier(string[] reels)
        {
            int cherryCount = countCherries(reels);
            if (cherryCount == 1) return 2;
            if (cherryCount == 2) return 3;
            if (cherryCount == 3) return 4;
            return 0;
        }

        private void outputMessage(double bet, double outcome)
        {
            if (outcome > 0)
                resultLabel.Text = String.Format("You bet {0:C} and won {1:C}", bet, outcome);
            else
                resultLabel.Text = String.Format("Sorry, you lost {0:C}", bet);
        }

        private void outputAmountOfMoney()
        {
            moneyLabel.Text = String.Format("Player's Money: {0:C}", ViewState["Money"]);
        }

        private void changeAmountOfMoney(double bet, double outcome)
        {
            double money = double.Parse(ViewState["Money"].ToString());
            money -= bet;
            money += outcome;
            ViewState.Add("Money", money);
        }
    }
}