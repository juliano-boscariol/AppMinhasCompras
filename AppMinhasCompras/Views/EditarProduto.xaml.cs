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
            //Programa��o do Update
            Produto produto_anexado = BindingContext as Produto;

            produto_anexado.Descricao = txt_descricao.Text;
            produto_anexado.Quantidade = Convert.ToDouble(txt_quantidade.Text);
            produto_anexado.Preco = Convert.ToDouble(txt_preco.Text);
            produto_anexado.Categoria = picker_categoria.SelectedItem?.ToString();

            /*Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
                Categoria = picker_categoria.SelectedItem?.ToString()
            }; Esse � jeito que estava antes e n�o estava atualizando o campo Categoria
            que foi criado posteriormente*/

            await App.DB.Update(produto_anexado);
            await DisplayAlert("Sucesso!", "Seu produto foi atualizado!", "OK");
            await Navigation.PopAsync(); // Retorna � p�gina inicial

        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}