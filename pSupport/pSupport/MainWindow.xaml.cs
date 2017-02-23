using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Data;


namespace pSupport
{

    public partial class MainWindow : Window
    {
   
        public static class Constants { public const string connect = "Data Source=vrsqladhoc;Initial Catalog=LoanAdminWrite;Integrated Security=True"; }

        DatabaseConnection objConnect;
        string conString; 
        DataSet ds;
        DataRow dRow;
        int MaxRows;
        int inc = 0;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();

            Style s = new Style();
            s.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
            tabMain.ItemContainerStyle = s;

            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

     

        
        private void btnLeftMenuHide_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbHideLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);
            gridNav.Visibility = Visibility.Visible;
        }

        private void btnLeftMenuShow_Click(object sender, RoutedEventArgs e)
        {

            ShowHideMenu("sbShowLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);
            gridNav.Visibility = Visibility.Collapsed;
        }

        private void ShowHideMenu(string Storyboard, Button btnShow, StackPanel pnl)
        {
            Storyboard sb = Resources[Storyboard] as Storyboard;
            sb.Begin(pnl);

            if (Storyboard.Contains("Show"))
            {
              
                btnShow.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (Storyboard.Contains("Hide"))
            {
               
                btnShow.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void ShowHideMenu(string Storyboard, Button btnHide, Button btnShow, StackPanel pnl)
        {
            Storyboard sb = Resources[Storyboard] as Storyboard;
            sb.Begin(pnl);

            if (Storyboard.Contains("Show"))
            {
                btnHide.Visibility = System.Windows.Visibility.Visible;
                btnShow.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (Storyboard.Contains("Hide"))
            {
               // btnHide.Visibility = System.Windows.Visibility.Hidden;
                btnShow.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnLeftMenuHide_Click_1(object sender, RoutedEventArgs e)
        {
            gridNav.Visibility = Visibility.Visible;
            ShowHideMenu("sbHideLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);
            tabHome.IsSelected = true;
        }

        private void btnProjects_Click(object sender, RoutedEventArgs e)
        {
 

            gridNav.Visibility = Visibility.Visible;
            ShowHideMenu("sbHideLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);
            tabList.IsSelected = true;
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            gridNav.Visibility = Visibility.Visible;
            ShowHideMenu("sbHideLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);
            tabPeople.IsSelected = true;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            gridNav.Visibility = Visibility.Visible;
            ShowHideMenu("sbHideLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);
            tabSearch.IsSelected = true;
        }

     

        private void LoadData()
        {
            try
            {
                objConnect = new DatabaseConnection();
                conString = Constants.connect;

                objConnect.connection_String = conString;
                objConnect.Sql = "select * from PS_Projects ";
                ds = objConnect.GetConnection;

                MaxRows = ds.Tables[0].Rows.Count;

                NavigateRecords();
 
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);

            }
        }


        private void NavigateRecords()
        {
            dRow = ds.Tables[0].Rows[inc];
            txtID.Text = dRow.ItemArray.GetValue(0).ToString();
            txtName.Text = dRow.ItemArray.GetValue(1).ToString();
            txtManager.Text = dRow.ItemArray.GetValue(2).ToString();
            txtDepartment.Text = dRow.ItemArray.GetValue(3).ToString();
            txtEmail.Text = dRow.ItemArray.GetValue(4).ToString();
            txtProjectName.Text = dRow.ItemArray.GetValue(5).ToString();
            txtProcessType.Text = dRow.ItemArray.GetValue(6).ToString();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if(inc != MaxRows -1)
            {
                inc++;
                NavigateRecords(); 

            }
            else
            {
                MessageBox.Show("No More Rows"); 
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if(inc > 0)
            {
                inc--;
                NavigateRecords(); 

            }
            else
            {
                MessageBox.Show("First Record"); 
            }
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            if(inc != MaxRows -1)
            {
                inc = MaxRows - 1;
                NavigateRecords(); 
            }

        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            if (inc != 0)
            {

                inc = 0;
                NavigateRecords();

            }
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            txtID.Clear(); 
            txtName.Clear();
            txtManager.Clear();
            txtEmail.Clear();
            txtDepartment.Clear();
            txtProcessType.Clear();
            txtProjectName.Clear();

            btnAddNew.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = ds.Tables[0].Rows[inc];

            row[0] = txtID.Text;
            row[1] = txtName.Text;
            row[2] = txtManager.Text;
            row[3] = txtEmail.Text;
            row[4] = txtDepartment.Text;

            try
            {

                objConnect.UpdateDatabse(ds); 

                MessageBox.Show("Record Updated");

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);

            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DataRow row = ds.Tables[0].NewRow();
         
            row[1] = txtName.Text;
            row[2] = txtManager.Text;
            row[3] = txtManager.Text;
            row[4] = txtDepartment.Text;

            ds.Tables[0].Rows.Add(row);


            try
            {
                objConnect.UpdateDatabse(ds);
                MaxRows = MaxRows + 1;
                inc = MaxRows - 1; 
                 
                MessageBox.Show("Database updated");
            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);

            }

            btnCancel.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnAddNew.IsEnabled = true; 
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
 

            NavigateRecords();

            btnCancel.IsEnabled = false;
            btnSave.IsEnabled = false;
            btnAddNew.IsEnabled = true;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ds.Tables[0].Rows[inc].Delete();
                objConnect.UpdateDatabse(ds);
                MaxRows = ds.Tables[0].Rows.Count;
                inc--;

                NavigateRecords();



                MsgForm n = new MsgForm("Record Deleted");
                n.Show();

 

            }
            catch (Exception err)
            {

                MessageBox.Show(err.Message);
            }
             

        }

        private void Window_Initialized(object sender, EventArgs e)
        {
          
        }

       
    } ///End Class


} //End Namespace
