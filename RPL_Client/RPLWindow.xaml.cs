using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RPL_Client
{
    /// <summary>
    /// Логика взаимодействия для RPLWindow.xaml
    /// </summary>
    
    public partial class RPLWindow : Window
    {
        public string cookie;
        public dynamic playerdata;
        public RPLWindow()
        {
            InitializeComponent();
            Show();
        }
        public RPLWindow(LoginWindow owner, dynamic playerdata, string cookie)
        {
            InitializeComponent();

            Network.Cookie = cookie;

            this.cookie = cookie;
            this.playerdata = playerdata;
            Console.WriteLine(cookie);
            Owner = owner;

            //L_Nickname.Text = this.playerdata.Nickname;
            L_Steam.Content = this.playerdata.Steam;

            if (this.playerdata.Steam_Status == "1")
            {
                L_SteamStatus.Foreground = Brushes.Lime;
                L_SteamStatus.Content = "Verified";
            }
            else
            {
                L_SteamStatus.Foreground = Brushes.Yellow;
                L_SteamStatus.Content = "UnVerified";
            }

            //Запрос на список друзей
            Friends.UpdateFriendList();



            Show();          
        }
        private void BT_Logout_Click(object sender, RoutedEventArgs e)
        {
            Hide();       
            Owner.Show();
            Close();
        }

        private void BT_PlayAsSurvivor_Click(object sender, RoutedEventArgs e)
        {
            BT_PlayAsSurvivor.Visibility = Visibility.Hidden;
            BT_PlayAsKiller.Visibility = Visibility.Hidden;

            L_Searching.Visibility = Visibility.Visible;
            BT_Cancel.Visibility = Visibility.Visible;

        }

        private void BT_PlayAsKiller_Click(object sender, RoutedEventArgs e)
        {
            BT_PlayAsKiller.Visibility = Visibility.Hidden;
            BT_PlayAsSurvivor.Visibility = Visibility.Hidden;

            L_Searching.Visibility = Visibility.Visible;
            BT_Cancel.Visibility = Visibility.Visible;
        }

        private void BT_Cancel_Click(object sender, RoutedEventArgs e)
        {
            L_Searching.Visibility = Visibility.Hidden;
            BT_Cancel.Visibility = Visibility.Hidden;

            BT_PlayAsKiller.Visibility = Visibility.Visible;
            BT_PlayAsSurvivor.Visibility = Visibility.Visible;
        }

        private void RPLWindow_Move(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
    }
}
