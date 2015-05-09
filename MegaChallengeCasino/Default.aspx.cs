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
            
        }

        protected void pullLeverButton_Click(object sender, EventArgs e)
        {
            displayImage(reelOneImage);
            displayImage(reelTwoImage);
            displayImage(reelThreeImage);
        }

        Random random = new Random();
        string[] reels = new string[3];

        private string generateRandomImage()
        {
            string[] images = new string[] { "Strawberry", "Bar", "Lemon", "Bell", "Clover", 
                "Cherry", "Diamond", "Orange", "Seven", "HorseShoe", "Plum", "Watermelon" };
            return images[random.Next(11)];
        }

        private string selectImage(string imageName)
        {
            string imageURL = "~/Images/" + imageName + ".png";
            return imageURL;
        }

        private void displayImage(Image reelName)
        {
            string imageName = generateRandomImage();
            string imageURL = selectImage(imageName);
            addImageToArray(imageName, reelName);
            reelName.ImageUrl = imageURL;
        }

        private void addImageToArray(string imageName, Image reelName)
        {
            if (reelName.ID == "reelOneImage")
                reels[0] = imageName;
            else if (reelName.ID == "reelTwoImage")
                reels[1] = imageName;
            else if (reelName.ID == "reelThreeImage")
                reels[2] = imageName;
        }

        private void valueOfPull()
        {

        }
    }
}