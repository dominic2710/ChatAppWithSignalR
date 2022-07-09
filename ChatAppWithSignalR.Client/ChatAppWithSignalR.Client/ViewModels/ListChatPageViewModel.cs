using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppWithSignalR.Client.ViewModels
{
    public class ListChatPageViewModel: INotifyPropertyChanged, IQueryAttributable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ServiceProvider _serviceProvider;

        public ListChatPageViewModel(ServiceProvider serviceProvider)
        {
            UserInfo = new User();
            UserFriends = new ObservableCollection<User>();
            LastestMessages = new ObservableCollection<LastestMessage>();

            RefreshCommand = new Command(() =>
            {
                Task.Run(async () =>
                {
                    IsRefreshing = true;
                    await GetListFriends();
                }).GetAwaiter().OnCompleted(() =>
                {
                    IsRefreshing = false;
                });
            });

            OpenChatPageCommand = new Command<int>(async (param) =>
            {
                await Shell.Current.GoToAsync($"ChatPage?fromUserId={UserInfo.Id}&toUserId={param}");
            });

            this._serviceProvider = serviceProvider;
        }

        private User userInfo;
        private ObservableCollection<User> userFriends;
        private ObservableCollection<LastestMessage> lastestMessages;
        private bool isRefreshing;

        async Task GetListFriends()
        {
            var response = await _serviceProvider.CallWebApi<int, ListChatInitializeResponse>
                ("/ListChat/Initialize", HttpMethod.Post, UserInfo.Id);

            if (response.StatusCode == 200)
            {
                UserInfo = response.User;
                UserFriends = new ObservableCollection<User>(response.UserFriends);
                LastestMessages = new ObservableCollection<LastestMessage>(response.LastestMessages);
            }
            else
            {
                await AppShell.Current.DisplayAlert("ChatApp", response.StatusMessage, "OK");
            }
        }

        public void Initialize()
        {
            Task.Run(async () =>
            {
                IsRefreshing = true;
                await GetListFriends();
            }).GetAwaiter().OnCompleted(() =>
            {
                IsRefreshing = false;
            });
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query == null || query.Count == 0) return;

            UserInfo.Id = int.Parse(HttpUtility.UrlDecode(query["userId"].ToString()));
            

        }

        public User UserInfo
        {
            get { return userInfo; }
            set { userInfo = value; OnPropertyChanged(); }
        }
        public ObservableCollection<User> UserFriends
        {
            get { return userFriends; }
            set { userFriends = value; OnPropertyChanged(); }
        }

        public ObservableCollection<LastestMessage> LastestMessages
        {
            get { return lastestMessages; }
            set { lastestMessages = value; OnPropertyChanged(); }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged(); }
        }

        public ICommand RefreshCommand { get; set; }        

        public ICommand OpenChatPageCommand { get; set; }
    }
}
