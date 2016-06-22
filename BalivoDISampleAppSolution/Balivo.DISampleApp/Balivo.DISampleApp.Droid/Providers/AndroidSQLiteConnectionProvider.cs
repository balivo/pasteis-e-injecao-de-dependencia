using Balivo.DISampleApp.Droid.Providers;
using Balivo.DISampleApp.Providers;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidSQLiteConnectionProvider))]

namespace Balivo.DISampleApp.Droid.Providers
{
    sealed class AndroidSQLiteConnectionProvider : ISQLiteConnectionProvider
    {
        public AndroidSQLiteConnectionProvider()
        {

        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(new SQLitePlatformAndroid(), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "DISampleDatabase.db3"));
        }
    }
}