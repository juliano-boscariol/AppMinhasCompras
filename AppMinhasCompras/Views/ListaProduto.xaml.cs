using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AppMinhasCompras.Models;

namespace AppMinhasCompras.Views;


public partial class ListaProduto : ContentPage
{
	/* Ao inv�s do ListView, utilizamos o ObservableCollection, pois possui
	maior integra��o com a interface gr�fica = Atualiza��es autom�ticas na tela */
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

	public ListaProduto()
	{
		InitializeComponent();

		lista_produtos.ItemsSource = lista;
	}

	// OnAppearing sempre aparece quando uma tela � chamada
    protected async override void OnAppearing()
    {
		try
		{
			lista.Clear(); // limpa a ObservableCollection
			
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

	// Evento referente � barra de pesquisa
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
		try
		{
			// A vari�vel "e" diz respeito ao texto digitado na busca
			string q = e.NewTextValue;

			lista_produtos.IsRefreshing = true; //IsRefreshing � o �cone de looping carregando

			lista.Clear(); // impede o ac�mulo dos itens

			List<Produto> tmp = await App.DB.Search(q);

			tmp.ForEach(i => lista.Add(i));
		}
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
		finally
		{
			lista_produtos.IsRefreshing = false;
		}
    }

    private void Clicked_Somar(object sender, EventArgs e)
    {
		try
		{
			double soma = lista.Sum(i => i.Total);

			string msg = $"O total dos produtos � {soma:C}";

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

			// Agora est� selecionado o produto que dever� ser removido
			Produto p = selecionado.BindingContext as Produto;

			//Confirma��o da exclus�o
			bool confirm = await DisplayAlert(
				"Tem certeza?", $"Deseja mesmo remover {p.Descricao}?", "Sim", "N�o");

			if (confirm) {
				await App.DB.Delete(p.Id);
				//se deixar apenas assim, n�o remover� da ListView
				//Adicionar o c�digo abaixo
				lista.Remove(p);
			}
		}
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void lista_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		try
		{
			Produto p = e.SelectedItem as Produto;

			// Ir� mandar as informa��es do produto p (selecionado e) para a p�gina de Edi��o
			Navigation.PushAsync(new Views.EditarProduto
			{
				BindingContext = p,
			});
		}
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void lista_produtos_Refreshing(object sender, EventArgs e)
    {
        try //Recarrega a lista quando a tela � puxada para baixo e add o �cone do looping
        {
            lista.Clear();

            List<Produto> tmp = await App.DB.GetAll();

            tmp.ForEach(i => lista.Add(i));

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
		finally // faz o �cone de carregando sair depois que a p�gina for atualizada
		// o finally � executado tanto se cair no try como no catch
		{
			lista_produtos.IsRefreshing = false;
		}
    }
}