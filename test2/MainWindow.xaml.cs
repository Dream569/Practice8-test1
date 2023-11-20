using IPractice;
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

namespace test2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<IHuman> _humen = new List<IHuman>() {
            new Father("Апостолов", "Денич", 43),
            new Student("Васильев", "Михаил", 18),
            new Student("Степанов", "Александр", 17)
        };
        private bool _ignoreMemberSelectonEvent = false;

        public MainWindow()
        {
            InitializeComponent();

            HumenBox.ItemsSource = _humen;
        }

        private void OnOneHumanSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
            {
                RemoveButton.IsEnabled = false;
                CloneButton.IsEnabled = false;
                TribeBox.IsEnabled = true;
                return;
            }

            var selected = (IHuman)e.AddedItems[0];

            SurnameBox.Text = selected.Surname;
            NameBox.Text = selected.Name;
            AgeBox.Text = selected.Age.ToString();
            TribeBox.SelectedIndex = selected is Student ? 0 : 1;
            TribeBox.IsEnabled = false;
            ApplyButton.Content = "Пересоздать";

            RemoveButton.IsEnabled = true;
            CloneButton.IsEnabled = true;
        }

        private void OnSortButtonClicked(object sender, RoutedEventArgs e)
        {
            HumenBox.ItemsSource = null;
            _humen.Sort((a, b) => ((IComparable)a).CompareTo(b));
            HumenBox.ItemsSource = _humen;
        }

        private void OnAddButtonClicked(object sender, RoutedEventArgs e)
        {
            TribeBox.IsEnabled = true;
            ApplyButton.Content = "Добавить";

            HumenBox.SelectedIndex = -1;
        }

        private void OnRemoveButtonClicked(object sender, RoutedEventArgs e)
        {
            IHuman human = HumenBox.SelectedItem as IHuman;

            RemoveHuman(human);
        }

        private void OnApplyButtonClicked(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(AgeBox.Text, out var age) || age <= 0)
            {
                MessageBox.Show("Такой возраст неправильный");
                return;
            }

            IHuman human;

            switch (TribeBox.SelectedIndex)
            {
                case 0:
                    human = new Student(SurnameBox.Text.Trim(), NameBox.Text.Trim(), age);
                    break;
                case 1:
                    human = new Father(SurnameBox.Text.Trim(), NameBox.Text.Trim(), age);
                    break;
                default:
                    human = null;
                    break;
            }

            _humen.Add(human);

            if (HumenBox.SelectedIndex != -1)
            {
                var oldHuman = HumenBox.SelectedItem as IHuman;
                RemoveHuman(oldHuman);
            }
            else
            {
                HumenBox.Items.Refresh();
                UpdateMembersList();
            }
        }
        private void OnCloneButtonClicked(object sender, RoutedEventArgs e)
        {
                _humen.Add(((ICloneable)HumenBox.SelectedItem).Clone() as IHuman);

                HumenBox.Items.Refresh();
                UpdateMembersList();
        }

        private void RemoveHuman(IHuman human)
        {
            _humen.Remove(human);

            HumenBox.Items.Refresh();
            UpdateMembersList();
        }

        private void UpdateMembersList()
        {
            _ignoreMemberSelectonEvent = true;
            _ignoreMemberSelectonEvent = false;
        }


        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработчик: Мелехин Дмитрий Создать интерфейс - человек. Создать классы – студент и студент-отец семейства. Классы должны включать конструкторы, функцию для формирования строки информации о студенте. Сравнение производить по фамилии.");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
                Close();
        }
    }
}
