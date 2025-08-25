using SQLite;

namespace AppMinhasCompras.Models
{
   
    // Na Agenda 01 - Organizamos as pastas e programamos a classe do Produto
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Quantidade { get; set; }
        public double Preco {  get; set; }
    }
}
