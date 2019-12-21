using System;
using System.Collections.Generic;
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

namespace ProductCategory
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Category> Categories { get; set; } = new List<Category>();
        private List<Product> Products { get; set; } = new List<Product>();
        private Predicate<string> PredicateDatabase { get; set; }
        private bool IsSuccessfull { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            using (var context = new Context())
            {
                Categories = context.Categories.ToList();
                Products = context.Products.ToList();
            }
        }


        private void ShowCategories(object sender, RoutedEventArgs e)
        {
            productsListView.Visibility = Visibility.Collapsed;
            categoriesListView.Visibility = Visibility.Visible;
        }

        private void ShowProducts(object sender, RoutedEventArgs e)
        {
            categoriesListView.Visibility = Visibility.Collapsed;
            productsListView.Visibility = Visibility.Visible;
            createCategoryButton.Visibility = Visibility.Collapsed;
            createProductButton.Visibility = Visibility.Visible;
        }

        private void CreateCategory(object sender, RoutedEventArgs e)
        {
            productGrid.Visibility = Visibility.Collapsed;
            categoryGrid.Visibility = Visibility.Visible;
            PredicateDatabase = new Predicate<string>(IsAddedCategory);
            PredicateDatabase.BeginInvoke(categoryNameTextBox.Text, ProcessAddCategory, null);
            if(IsSuccessfull)
            {
                MessageBox.Show("Категория добавлена");
                productsButton.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Ошибка добавления категории");
            }
        }

        private void ProcessAddCategory(IAsyncResult result)
        {
            IsSuccessfull = PredicateDatabase.EndInvoke(result);
        }

        private bool IsAddedCategory(string categoryName)
        {
            try
            {
                using (var context = new Context())
                {
                    context.Categories.Add(new Category { Name = categoryName });
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        private void CreateProduct(object sender, RoutedEventArgs e)
        {
            categoryGrid.Visibility = Visibility.Collapsed;
            productGrid.Visibility = Visibility.Visible;
            PredicateDatabase = new Predicate<string>(IsAddedProduct);
            PredicateDatabase.BeginInvoke(categoryNameTextBox.Text, ProcessAddProduct, null);
            if (IsSuccessfull)
            {
                MessageBox.Show("Продукт добавлен");
            }
            else
            {
                MessageBox.Show("Ошибка добавления продукта");
            }
        }

        private void ProcessAddProduct(IAsyncResult result)
        {
            IsSuccessfull = PredicateDatabase.EndInvoke(result);
        }

        private bool IsAddedProduct(string productName)
        {
            try
            {
                using (var context = new Context())
                {
                    context.Products.Add(new Product { Name = productName });
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
