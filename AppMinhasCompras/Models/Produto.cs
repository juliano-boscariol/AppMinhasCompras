using SQLite;

namespace AppMinhasCompras.Models
{

    // Na Agenda 01 - Organizamos as pastas e programamos a classe do Produto
    public class Produto
    {

        // Agenda 05 - Criando a validação dos campos Descrição, Quantidade e Preço

        string _descricao;
        double _quantidade;
        double _preco;
        string _categoria;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao {
            get => _descricao; 
            set
            {
                if (value == null) // se for vazio, dispara a exceção
                {
                    throw new Exception("Por favor, preencha a descrição do produto.");
                }
                _descricao = value;
            }
        }
        public double Quantidade
        {
            get => _quantidade;
            set
            {
                if (value == 0) // se for zero, dispara a exceção
                {
                    throw new Exception("Por favor, preencha a quantidade do produto.");
                }
                _quantidade = value;
            }
        }
        public double Preco
        {
            get => _preco;
            set
            {
                if (value == 0) // se for zero, dispara a exceção
                {
                    throw new Exception("Por favor, preencha o preço do produto.");
                }
                _preco = value;
            }
        }

        public string Categoria
        {
            get => _categoria;
            set
            {
                if (value == null) // se for vazio, dispara a exceção
                {
                    throw new Exception("Por favor, selecione a categoria do produto.");
                }
                _categoria = value;
            }
        }


        // O "=>" declara o que o get irá retornar
        public double Total { get => Quantidade * Preco; }
    }
}
