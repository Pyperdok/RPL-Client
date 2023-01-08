using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;

namespace RPL_Client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static RPLWindow rplwindow;
        
        public LoginWindow()
        {       
            InitializeComponent();
            
        }



        //Button Login Click Event
        private void BT_Login_Click(object sender, RoutedEventArgs e)
        {
            new RPLWindow();
            return;
            string login = TB_Login.Text;
            string password = PB_Password.Password;

            string body = "{" + $"\"login\": \"{login}\", \"password\": \"{password}\"" + "}";
            
           Dictionary<string, string> auth = Network.http("http://rpl.com/login?action=login", "POST", body);

            if (auth["StatusCode"] == "OK")
            {
                Console.WriteLine("You are logged");

                dynamic json = JsonConvert.DeserializeObject(auth["Data"]);

                Hide();

               // dynamic rr = Network.http($"http://rpl.com/api/friends/GetFriendlist", "POST", $"{auth["Cookie"]}");

                rplwindow = new RPLWindow(this, json, auth["Cookie"]);         
            }       
            else
            {
                //Unauthorized
                Console.WriteLine("Login/Password uncorrect");
            }
        }

        //Button Minimize Events
        private void Button_Minimize_Click(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;         
        }
        private void Button_Minimize_Enter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            BR_Minimize_Color.Opacity = 0.17;
        }

        private void Button_Minimize_Leave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            BR_Minimize_Color.Opacity = 0;
        }

        //Button Close Events
        private void Button_Close_Enter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            BR_Close_Color.Opacity = 0.37;
        }

        private void Button_Close_Leave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            BR_Close_Color.Opacity = 0;
        }

        private void Button_Close_Click(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        //Hyperlinks Events
        private void TB_Cant_Sign_Enter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            TB_Cant_Sign.Foreground = Brushes.Red;
        }

        private void TB_Cant_Sign_Leave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            TB_Cant_Sign.Foreground = Brushes.White;
        }
        private void TB_Cant_Sign_Click(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://google.com");
        }

        private void TB_Create_Account_Enter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            TB_Create_Account.Foreground = Brushes.Red;
        }

        private void TB_Create_Account_Leave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            TB_Create_Account.Foreground = Brushes.White;
        }
        private void TB_Create_Account_Click(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://google.com");
        }

        //MoveForm Event
        private void LoginWindow_Move(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        //Enter default Login Press Events
        private void Button_Login_EnterDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
                BT_Login_Click(sender, e);
        }

        private void BT_Login_Enter(object sender, MouseEventArgs e)
        {
            BT_Login.Background = Brushes.Green;

        }

        private void BT_Login_Leave(object sender, MouseEventArgs e)
        {
            BT_Login.Background = Brushes.Transparent;
        }
    }
}
