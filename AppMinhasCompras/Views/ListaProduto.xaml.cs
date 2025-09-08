using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppMinhasCompras.Models;

namespace AppMinhasCompras.Views;


public partial class ListaProduto : ContentPage
{
	// Maior integração com a interface gráfica = Atualizações automáticas na tela
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

	public ListaProduto()
	{
		InitializeComponent();

		lista_produtos.ItemsSource = lista;
	}

	// OnAppearing sempre aparece quando uma tela é chamada
    protected async override void OnAppearing()
    {
		try
		{
			List<Produto> tmp = await App.DB.GetAll();

			// Para cada item na lista, chama-se a ObservableCollection
			tmp.ForEach(i => lista.Add(i));

			/* Ou seja, toda vez que a tela aparecer, busca-se a lista no SQL
			e abastesse-se a ObservableCollection da forma especificada em ForEach */
		}
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());
		}
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
    }

	// Evento referente à barra de pesquisa
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
		try
		{
			// A variável "e" diz respeito ao texto digitado na busca
			string q = e.NewTextValue;

			lista.Clear(); // impede o acúmulo dos itens

			List<Produto> tmp = await App.DB.Search(q);

			tmp.ForEach(i => lista.Add(i));
		}
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void Clicked_Somar(object sender, EventArgs e)
    {
		try
		{
			double soma = lista.Sum(i => i.Total);

			string msg = $"O total dos produtos é {soma:C}";

			DisplayAlert("Total dos Produtos", msg, "OK");
		}
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

	// Evento para remover um item
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			MenuItem selecionado = sender as MenuItem;

			// Agora está selecionado o produto que deverá ser removido
			Produto p = selecionado.BindingContext as Produto;

			//Confirmação da exclusão
			bool confirm = await DisplayAlert(
				"Tem certeza?", $"Deseja mesmo remover {p.Descricao}?", "Sim", "Não");

			if (confirm) {
				await App.DB.Delete(p.Id);
				//se deixar apenas assim, não removerá da ListView
				//Adicionar o código abaixo
				lista.Remove(p);
			}
		}
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}