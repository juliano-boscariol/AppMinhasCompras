// Adicionar esses dois usings abaixo
// No final, botão direito e "Remover e classificar usos"
using AppMinhasCompras.Models;
using SQLite;

namespace AppMinhasCompras.Helpers
{
    /* Observações
    O SQLite não deve ser encarado como um BD em si.
    Ele seria mais como um emulador, visto que não
    armazena as informações da melhor forma.
    Usos: apps sem necessidade de segurança, cash, etc.*/


    // Mudar de internal para public class
    public class SQLiteDatabaseHelper
    {
        // Criar o campo somente leitura
        readonly SQLiteAsyncConnection _connection;

        // Construtor sempre é chamado quando o objeto é instanciado.
        public SQLiteDatabaseHelper(string path) 
        {
            _connection = new SQLiteAsyncConnection(path);
            _connection.CreateTableAsync<Produto>().Wait();
        }

        // Declaração dos métodos Produto
        // Obs: void = método que não retorna nenhuma informação
        public Task<int> Insert(Produto p) //mudou de void para Task<int>
        {
            return _connection.InsertAsync(p);
        }
        public Task<List<Produto>> Update(Produto p)
        {
            //Não precisa escrever assim igual no php/SGBD - dá para simplificá-lo
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
            
            return _connection.QueryAsync<Produto>(sql,
                p.Descricao, p.Quantidade, p.Preco, p.Id);
        }
        public Task<int> Delete(int id) 
        {
            return _connection.Table<Produto>().DeleteAsync(i => i.Id == id);
        }


        // Para listar os produtos GetAll
        public Task<List<Produto>> GetAll()
        {
            return _connection.Table<Produto>().ToListAsync();
        }


        // Para fazer a busca Search
        public Task<List<Produto>> Search (string q)
        {
            string sql = "SELECT * Produto WHERE Descricao LIKE '%" + q + "%'";

            return _connection.QueryAsync<Produto>(sql);
        }


    }
}
