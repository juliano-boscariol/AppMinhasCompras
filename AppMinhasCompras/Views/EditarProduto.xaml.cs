using AppMinhasCompras.Models;

namespace AppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void Clicked_Salvar(object sender, EventArgs e)
    {
		try
		{
            //Programação do Update
            Produto produto_anexado = BindingContext as Produto;

            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
            };

            await App.DB.Update(p);
            await DisplayAlert("Sucesso!", "Seu produto foi atualizado!", "OK");
            await Navigation.PopAsync(); // Retorna à página inicial

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}