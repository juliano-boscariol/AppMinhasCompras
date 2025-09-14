using System.Globalization;
using AppMinhasCompras.Helpers;

namespace AppMinhasCompras
{

    /* Na Agenda 03, instanciamos a classe database neste arquivo para
     torná-la disponível em todo o app */
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _db; //campo
        public static SQLiteDatabaseHelper DB //propriedade
        {
            get
            {
                if (_db == null)
                {
                    /* como o arquivo do BD fica gravado em diferentes lugares de
                    acordo com a plataforma, temos que fazer esse comando abaixo. Ele
                    garante que a gente encontre o caminho completo até o arquivo*/
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.db3");

                    _db = new SQLiteDatabaseHelper(path);
                }
                return _db; //deve conter 1 instância da classe SQLiteDataBaseHelper
            }
        }
        public App()
        {
            InitializeComponent();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR"); //Add a Culture como pt-BR
            //Formata números, datas e R$.

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.ListaProduto());

        }
    }
}
