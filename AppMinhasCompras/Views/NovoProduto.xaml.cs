using System.Threading.Tasks;
using AppMinhasCompras.Models;

namespace AppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            //Programação do Insert
            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
                Categoria = picker_categoria.SelectedItem?.ToString()
            };

            await App.DB.Insert(p);
            await DisplayAlert("Sucesso!", "Seu produto foi cadastrado", "OK");
            await Navigation.PopAsync(); //Adicionei para voltar à página inicial também
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}