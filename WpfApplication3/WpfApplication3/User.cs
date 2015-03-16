using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication3
{
    class User
    {

        private String username;
        private int score;
        private bool guestPlayer;


        public User()
        {
            username = "Guest";
            guestPlayer = true;
            score = 0;
        }

        public User(String userData)
        {
            username = userData;
            guestPlayer = false;
            score = 0;
        }

        public void setUsername(String userData)
        {
            username = userData;
        }

        public String getUsername()
        {
            return username;
        }

        public void setGuestPlayer(bool isGuest)
        {
            guestPlayer = isGuest;
        }

        public bool isGuest()
        {
            return guestPlayer;
        }

        public void addScore(){
            score++;
        }

        public int getScore()
        {
            return score;
        }
    }
}
