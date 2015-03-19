using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication3
{
    public class User
    {

        private string username;
        private int score;
        private bool guestPlayer;

        //Constructor use when it is a guest player
        public User()
        {
            username = "Guest";
            guestPlayer = true;
            score = 0;
        }

        //Constructor use when it is a login player
        public User(string userData)
        {
            username = userData;
            guestPlayer = false;
            score = 0;
        }

        //Copy Constructor
        public User(User prevUser)
        {
            username = prevUser.username;
            score = 0;
            guestPlayer = false;
        }

        //Desctructor
        ~User()
        {
            username = "";
            score = 0;
            guestPlayer = false;
        }

        public void setUsername(string userData)
        {
            username = userData;
        }

        public string getUsername()
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
