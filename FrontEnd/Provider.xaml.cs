using System.Net.Http.Headers;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.ComponentModel;
using System;
using Backend.DB;

namespace FrontEnd
{
    public partial class Provider : Page, INotifyPropertyChanged
    {
        public Provider()
        {
            InitializeComponent();
        }
        void Signal(string prop) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public event PropertyChangedEventHandler PropertyChanged;
        public class ProviderModel
        {
            public int Idprovider { get; set; }

            public string Title { get; set; }

            public string Info { get; set; }

            public string Number { get; set; }

            public string Email { get; set; }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await CheckProviders();
        }

        private async Task CheckProviders()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7107/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Provider/providers");

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    List<ProviderModel> providers = JsonConvert.DeserializeObject<List<ProviderModel>>(responseContent);

                    lst.ItemsSource = providers;
                }
            }
        }

        private async Task DeleteSelectedProviderAsync()
        {
            if (lst.SelectedItem != null)
            {
                ProviderModel selectedProvider = (ProviderModel)lst.SelectedItem;

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить этого поставщика?", "Подтверждение удаления", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    int providerId = selectedProvider.Idprovider;

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://localhost:7107/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.DeleteAsync($"api/Provider/delprovider/{providerId}");

                        if (response.IsSuccessStatusCode)
                        {
                            List<ProviderModel> providers = (List<ProviderModel>)lst.ItemsSource;
                            providers.Remove(selectedProvider);
                            lst.ItemsSource = null;
                            lst.ItemsSource = providers;

                            MessageBox.Show("Поставщик успешно удален.");
                        }
                        else
                        {
                            MessageBox.Show("Не удалось удалить поставщика.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите поставщика для удаления.");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddProvider addprovider = new AddProvider();
            NavigationService.Navigate(addprovider);

        }
        private void Red_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Del_Click(object sender, RoutedEventArgs e)
        {
            await DeleteSelectedProviderAsync();
        }
    }
}
