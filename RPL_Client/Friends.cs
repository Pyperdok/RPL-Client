using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPL_Client
{
   static public class Friends
    {
        static public void UpdateFriendList()
        {
            string url = "http://rpl.com/api/friends/GetFriendlist";
           Dictionary<string, string> result = Network.http(url);
            Console.WriteLine(result["Data"]);
        }
        static public void AddFriend()
        {

        }
        static public void AcceptFriend()
        {

        }
        static public void RemoveFriend()
        {

        }
    }
}
