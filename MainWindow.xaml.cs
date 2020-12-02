using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoApp_v1._2.Controllers;
using ToDoApp_v1._2.Model;
using Autofac;
using System.Data.SQLite;
using ToDoApp_v1._2.Database;

namespace ToDoApp_v1._2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        




        DataDbContext _context;
        DataController _datacontroller = new DataController();
        ConnectDB db_context = new ConnectDB();
        //public readonly App _container;
        //private readonly DataDbContext _context;

        //private CollectionViewSource datalistViewSource;

        public MainWindow(DataDbContext context)
        {
            _context = context;
            
            //_context = context;

            InitializeComponent();
            
            //datalistViewSource =
            //(CollectionViewSource)FindResource(nameof(datalistViewSource));
        }

        private void GetList()
        {
            //_context.Datalists.Load();
            //_context.Itemlists.Load();

            //datalistViewSource = (CollectionViewSource)FindResource(nameof(datalistViewSource));
            //datalistViewSource.Source = _context.Datalists.Local.ToObservableCollection();
            //listDataGrid.AutoGenerateColumns = false;

            //listDataGrid.ItemsSource = _context.Datalists.Local.ToObservableCollection();

            //listDataGrid.ItemsSource = _datacontroller.GetAllList();
            listDataGrid.ItemsSource = db_context.GetAll("Datalists");
            
            //db_context.GetItem();
            itemsDataGrid.ItemsSource = db_context.GetItem(ListDataId);
            //itemsDataGrid.ItemsSource = _datacontroller.GetItem(ListDataId);
            listDataGrid.Items.Refresh();
            itemsDataGrid.Items.Refresh();
            // bind to the source

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            _context.Database.EnsureCreated();
            // load the entities into EF Core
            GetList();
            listDataGrid.SelectedItems.Clear();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            // clean up database connections
            _context.Dispose();
            base.OnClosing(e);
        }
        public void OpenListform(object s, RoutedEventArgs e) // open New windows to add new List
        {
            
            CreateListForm win2 = new CreateListForm();
            win2.ShowDialog();
            GetList();
            
        }
        
        public void AddItemform(object s, RoutedEventArgs e) // Add Item : open New windows to add new item
        {
            
            CreateItemForm win3 = new CreateItemForm();
            win3._ItemDataListId = ListDataId;
            win3.ShowDialog();

            GetList();
        }
        
        public void SelectListToEdit(object s, RoutedEventArgs e) // Edit: open new windows to edit list
        {
            
            var ListData = (s as FrameworkElement).DataContext as Datalist;
          
            CreateListForm winlist = new CreateListForm();
            winlist._ListId = ListData.DatalistId;
            winlist._ListName = ListData.Name;
            winlist._ListDescription = ListData.Description;

            winlist.ShowDialog();
            GetList();
        }
        
        public void SelectItemToEdit(object s, RoutedEventArgs e) // Edit: open to edit item
        {

            var ItemData = (s as FrameworkElement).DataContext as Itemlist;
            
            CreateItemForm winItem = new CreateItemForm();
            winItem._ItemId = ItemData.ItemlistId;
            winItem._ItemNames = ItemData.Name;
            winItem._ItemDetail = ItemData.Detailed;
            winItem._ItemStatuz = ItemData.Status;
            winItem._ItemDataListId = ItemData.DatalistId;
            winItem.ShowDialog();
            GetList();



        }
        public void DeleteList(object s, RoutedEventArgs e) // -------------------> DELETE data in List
        {
            var ListToDelete = (s as FrameworkElement).DataContext as Datalist;

            ListController _listController = new ListController();
            MessageBox.Show( _listController.DeleteList_Class(ListToDelete));
            
            GetList();
        }
        
        public void DeleteItem(object s, RoutedEventArgs e) // -------------------> DELETE data in ITem
        {
            var ItemToDelete = (s as FrameworkElement).DataContext as Itemlist;

            ItemController _itemController = new ItemController();
            MessageBox.Show( _itemController.DeleteItem_Class(ItemToDelete));
          
            GetList();
        }

        //public static int Global_ListData ;
        int ListDataId;
        public void View_ListData(object s, RoutedEventArgs e) // --------------> To View All Item Under the List
        {
            btn_CreateList.Visibility = Visibility.Hidden;
            btn_CreateItem.Visibility = Visibility.Visible;
            btn_BacktoListView.Visibility = Visibility.Visible;

            listDataGrid.Visibility = Visibility.Hidden;
            itemsDataGrid.Visibility = Visibility.Visible;

           

            var ListData = (s as FrameworkElement).DataContext as Datalist;
            ListDataId = ListData.DatalistId; //-------------------------------> send id to createItemForm

            GetList();
            //itemsDataGrid.ItemsSource = _datacontroller.GetItem(ListDataId);

        }
        public void BacktoListView(object s, RoutedEventArgs e) // --------------> back to list View data
        {
             
            btn_CreateList.Visibility = Visibility.Visible;
            btn_CreateItem.Visibility = Visibility.Hidden;
            btn_BacktoListView.Visibility = Visibility.Hidden;

            listDataGrid.Visibility = Visibility.Visible;
            itemsDataGrid.Visibility = Visibility.Hidden;

        }

        public void View_ItemData(object s, RoutedEventArgs e) // View The Detail Of the Item
        {

            ConnectDB Db_Context  = new ConnectDB();
            MessageBox.Show(Db_Context.GetAll("Datalists").ToString());
            /*var ListData = (s as FrameworkElement).DataContext as Itemlist;  
                _context.Datalists.Find(ListData.DatalistId).Name.ToString()   // -------------> example find or Search
            */
        }

    }
}
