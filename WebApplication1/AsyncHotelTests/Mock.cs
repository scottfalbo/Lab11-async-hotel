using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using AsyncHotel.Models;
using AsyncHotel.Data;
using AsyncHotel.Models.Api;
using System.Threading.Tasks;
using Xunit;

namespace AsyncHotelTests
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly AsyncDbContext _db;

        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _db = new AsyncDbContext(
                new DbContextOptionsBuilder<AsyncDbContext>()
                    .UseSqlite(_connection)
                    .Options);

            _db.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }

    }
}
