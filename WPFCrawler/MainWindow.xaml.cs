using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFCrawler {
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        private List<int> selectUId = new List<int>();//保存多选用户ID  
        private List<User> userList = new List<User>();//数据源

        public MainWindow() {
            InitializeComponent();
            this.DataBinding();

            // Remove limits from Service Point Manager
            ServicePointManager.MaxServicePoints = 999999;
            ServicePointManager.DefaultConnectionLimit = 999999;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
            ServicePointManager.CheckCertificateRevocationList = true;
            ServicePointManager.EnableDnsRoundRobin = true;
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBinding() {
            for (int i = 0; i < 5; i++) {
                User user = new User() {
                    UId = i + 1,
                    UserName = "username" + i,
                    Password = "password" + 1
                };
                userList.Add(user);
            }
            this.dataGrid.ItemsSource = userList;
        }

        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="user"></param>
        private void AddUser(User user) {
            userList.Add(user);
        }

        /// <summary>  
        /// 删除一个用户  
        /// </summary>  
        /// <param name="uid"></param>  
        private void DeleteUser(int uId) {
            userList.Remove(userList.Single(u => u.UId == uId));
        }


        #region 事件处理函数
        private void btnDelete(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            int uId = Convert.ToInt32(b.CommandParameter);
            this.DeleteUser(uId);
            this.dataGrid.Items.Refresh();
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            User user = new User() {
                UId = 3,
                UserName = "customer",
                Password = "1111"
            };
            this.AddUser(user);
            this.dataGrid.Items.Refresh();
        }

        private void ckbSelectedAll_Checked(object sender, RoutedEventArgs e) {
            this.dataGrid.SelectAll();
        }

        private void ckbSelectedAll_Unchecked(object sender, RoutedEventArgs e) {
            this.dataGrid.UnselectAll();
        } 
        #endregion

    }
}
