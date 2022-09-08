using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using static System.Console;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //we create a socket
        private const int PORT = 100;
        private static string id = "";
        private static string request = "";
        private static List<CheckBox> store = new List<CheckBox>();
        private static List<string> nicks = new List<string>();
        TextBoxOutputter outputter;
        private static List<string> targets = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            targets.Clear();
            outputter = new TextBoxOutputter(TestBox);
            SetOut(outputter);



        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            id = textbox_username.Text.ToString();

            ConnectToServer();
            Thread th2 = new Thread(new ThreadStart(ReceiveResponse));

            th2.Start();



            string msg1 = "0:" + id; // this is a hello msg to server to create a new client in server.

            SendString(msg1);
            connected1.Visibility = Visibility.Visible;
            refreshing();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {


            sending();
            refreshing();

        }


        private static void ConnectToServer()
        {
            int attempts = 0;


            while (!_clientSocket.Connected)
            {
                try
                {
                    attempts++;

                    Thread.Sleep(1000);
                    _clientSocket.Connect(IPAddress.Loopback, PORT);
                }
                catch (SocketException)
                {
                    ConnectToServer();
                }
                catch (InvalidOperationException) // when the server shutdown, these 'catch' help us to regenerate a socket for client.
                {
                    _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    ConnectToServer();
                }

            }


        }





        private static void SendString(string text)
        {

            byte[] buffer = Encoding.ASCII.GetBytes(text);
            _clientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);


        }

        private void ReceiveResponse()
        {
            var buffer = new byte[1024];
            while (true)
            {
                try
                {
                    int received = _clientSocket.Receive(buffer, SocketFlags.None);
                    if (received == 0) return;
                    var data = new byte[received];
                    Array.Copy(buffer, data, received);
                    string text = Encoding.ASCII.GetString(data);
                    string aaa132 = text.Split(':')[0];
                    if (text.ToLower() != "sent\n" && text.Split(':')[0] != "3121" && text.ToLower() != " not valid1\n")
                    {
                        WriteLine(text);
                    }
                    else if (text.Split(':')[0] == "3121")
                    {
                        string[] one = text.Split("3121:");
                        string nickname = one[1];
                        for (int i = 1; i < one.Length; i++)
                        {
                            if (!nicks.Contains(one[i]))
                            {
                                nicks.Add(one[i]);
                            }



                        }

                    }
                }



                catch (SocketException)
                {

                    ConnectToServer();
                }
            }



        }





        public class TextBoxOutputter : TextWriter
        {
            TextBox textBox = null;

            public TextBoxOutputter(TextBox output)
            {
                textBox = output;
            }

            public override void Write(char value)
            {
                base.Write(value);
                textBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    textBox.AppendText(value.ToString());
                }));
            }

            public override Encoding Encoding
            {
                get { return System.Text.Encoding.UTF8; }
            }
            public class ListToStringConverter : IValueConverter
            {

                public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
                {
                    if (targetType != typeof(string))
                        throw new InvalidOperationException("The target must be a String");

                    return String.Join(", ", ((List<string>)value).ToArray());
                }

                public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
                {
                    throw new NotImplementedException();
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {


            refreshing();
        }



        private void First_Checked(object sender, RoutedEventArgs e)
        {
            targets.Add((string)First.Content);
        }

        private void Second_Checked(object sender, RoutedEventArgs e)
        {
            targets.Add((string)Second.Content);
        }

        private void Third_Checked(object sender, RoutedEventArgs e)
        {
            targets.Add((string)Third.Content);
        }

        private void Fourth_Checked(object sender, RoutedEventArgs e)
        {
            targets.Add((string)Fourth.Content);
        }

        private void Fifth_Checked(object sender, RoutedEventArgs e)
        {
            targets.Add((string)Fifth.Content);
        }

        private void Sixth_Checked(object sender, RoutedEventArgs e)
        {
            targets.Add((string)Sixth.Content);
        }

        private void Seventh_Checked(object sender, RoutedEventArgs e)
        {
            targets.Add((string)Seventh.Content);
        }

        private void Eighth_Checked(object sender, RoutedEventArgs e)
        {
            targets.Add((string)Eighth.Content);
        }
        private void sent(string xd)
        {
            SendString("1:" + targets[0] + ":" + xd);
        }

        private void First_Unchecked(object sender, RoutedEventArgs e)
        {
            targets.Remove((string)First.Content);
        }

        private void Second_Unchecked(object sender, RoutedEventArgs e)
        {
            targets.Remove((string)Second.Content);
        }

        private void Third_Unchecked(object sender, RoutedEventArgs e)
        {
            targets.Remove((string)Third.Content);
        }

        private void Fourth_Unchecked(object sender, RoutedEventArgs e)
        {
            targets.Remove((string)Fourth.Content);
        }

        private void Fifth_Unchecked(object sender, RoutedEventArgs e)
        {
            targets.Remove((string)Fifth.Content);
        }

        private void Sixth_Unchecked(object sender, RoutedEventArgs e)
        {
            targets.Remove((string)Sixth.Content);
        }

        private void Seventh_Unchecked(object sender, RoutedEventArgs e)
        {
            targets.Remove((string)Seventh.Content);
        }

        private void Eighth_Unchecked(object sender, RoutedEventArgs e)
        {
            targets.Remove((string)Eighth.Content);
        }

        private void textbox_msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                sending();
            }
        }
        private void sending()
        {

            request = textbox_msg.Text.ToString();
            TestsBox.AppendText(request + Environment.NewLine);
            for (int i = 0; i < targets.Count; i++)
            {
                string msg = "1:" + targets[i] + ":" + request; // when server see 1, it will understand it is a text msg
                SendString(msg);
                Thread.Sleep(10);
            }


            textbox_msg.Clear();

        }
        private void refreshing()
        {
            store.Remove(First);
            store.Remove(Second);
            store.Remove(Third);
            store.Remove(Fourth);
            store.Remove(Fifth);
            store.Remove(Sixth);
            store.Remove(Seventh);
            store.Remove(Eighth);
            store.Add(First);
            store.Add(Second);
            store.Add(Third);
            store.Add(Fourth);
            store.Add(Fifth);
            store.Add(Sixth);
            store.Add(Seventh);
            store.Add(Eighth);
            for (int i = 0; i < nicks.Count; i++)
            {

                store[i].Visibility = Visibility.Visible;
                store[i].Content = nicks[i].ToString();
            }
        }
    }
}
