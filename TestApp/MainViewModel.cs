using GalaSoft.MvvmLight;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json.Linq;
using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace TestApp
{
    public class MainViewModel : ViewModelBase

    {
        private string tekst = "";
        public string Tekst {
            get {
                return tekst;
            }
            set {
                tekst = value;
                RaisePropertyChanged("Tekst");
            }
        }
        private readonly CoreDispatcher _dispatcher;
        public MainViewModel()
        {
            var socket = IO.Socket("http://192.168.3.80/");
            socket.Connect();
            // whenever the server emits "login", print the login message
            socket.On("tempUpdate", data => {
                

                // get the json data from the server message
                var jobject = data as JToken;

                // get the number of users
                var numUsers = jobject.Value<int>("numUsers");
                Tekst = numUsers.ToString();
                // display the welcome message...
            });

            Tekst = "Start";
            _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;

            var connection = new HubConnection("http://internetuptime.azurewebsites.net/signalr");

            var hubProxy = connection.CreateHubProxy("UptimeHub");
            hubProxy.On<string>("internetUpTime", UpdateTime);

            connection.Start();
        }

        private string uptime ="";
        public string Uptime {
            get {
                return uptime;
            }
            set {
                uptime = value;
                RaisePropertyChanged("Uptime");
            }
        }

        private async void UpdateTime(string s)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => Uptime = s);
        }
    }
}
