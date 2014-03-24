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
using WPFCrawler.Entities;

namespace WPFCrawler
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<int> selectUId = new List<int>();//保存多选用户ID  
        private List<Link> linkList = new List<Link>();//数据源

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
                Link link = new Link() {
                    Id = (i + 1).ToString(),
                    Title = "username" + i,
                    Url = "password" + 1
                };
                linkList.Add(link);
            }
            this.dataGrid.ItemsSource = linkList;
        }

        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="user"></param>
        private void AddLink(Link link) {
            linkList.Add(link);
        }

        /// <summary>  
        /// 删除一个链接  
        /// </summary>  
        /// <param name="uid"></param>  
        private void DeleteUser(string Id) {
            linkList.Remove(linkList.Single(u => u.Id == Id));
        }


        #region 事件处理函数
        private void btnDelete(object sender, RoutedEventArgs e) {
            Button b = sender as Button;
            string id = b.CommandParameter.ToString();
            this.DeleteUser(id);
            this.dataGrid.Items.Refresh();
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            Link link = new Link() {
                Id = "3",
                Title = "customer",
                Url = "1111"
            };
            this.AddLink(link);
            this.dataGrid.Items.Refresh();
        }

        private void ckbSelectedAll_Checked(object sender, RoutedEventArgs e) {
            this.dataGrid.SelectAll();
        }

        private void ckbSelectedAll_Unchecked(object sender, RoutedEventArgs e) {
            this.dataGrid.UnselectAll();
        }
        #endregion

        private void chkSelectAll_Checked(object sender, RoutedEventArgs e) {
            for (int i = 0; i < this.dataGrid.Items.Count; i++) {
                DataGridRow dataGridRow = this.dataGrid.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                if (dataGridRow != null) {
                    FrameworkElement cellObj = dataGrid.Columns[0].GetCellContent(dataGridRow);
                    if (cellObj != null) {
                        ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(cellObj);
                        //DataTemplate myDataTemplate = myContentPresenter.te;
                        CheckBox checkBox = (CheckBox)myDataTemplate.FindName("chkSelected", myContentPresenter);
                        checkBox.IsChecked = true;
                    }
                }
            }

        }

        private void chkUnselectAll_Checked(object sender, RoutedEventArgs e) {

        }

        private void chkInverseSelect_Checked(object sender, RoutedEventArgs e) {

        }

        private void dataGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            if (IsUnderTabHeader(e.OriginalSource as DependencyObject))
                CommitTables(this.dataGrid);
        }

        private bool IsUnderTabHeader(DependencyObject control) {
            if (control is TabItem)
                return true;
            DependencyObject parent = VisualTreeHelper.GetParent(control);
            if (parent == null)
                return false;
            return IsUnderTabHeader(parent);
        }

        private void CommitTables(DependencyObject control) {
            if (control is DataGrid) {
                DataGrid grid = control as DataGrid;
                grid.CommitEdit(DataGridEditingUnit.Row, true);
                return;
            }
            int childrenCount = VisualTreeHelper.GetChildrenCount(control);
            for (int childIndex = 0; childIndex < childrenCount; childIndex++)
                CommitTables(VisualTreeHelper.GetChild(control, childIndex));
        }

        private void Item_Unchecked(object sender, RoutedEventArgs e) {

        }

        private void Item_Checked(object sender, RoutedEventArgs e) {

        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++) {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
