using SQLite.Net;

namespace Balivo.DISampleApp.Providers
{
    public interface ISQLiteConnectionProvider
    {
        SQLiteConnection GetConnection();
    }
}
