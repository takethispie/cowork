using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Npgsql;

namespace coworkpersistence.Handlers {

    public class PostgresHandler : ISqlDbHandler {

        private readonly NpgsqlConnection connection;
        private DbDataReader reader;


        public PostgresHandler(string connectionString) {
            if (string.IsNullOrEmpty(connectionString)) throw new Exception("Connection String is Empty");
            this.connectionString = connectionString;
            try {
                connection = new NpgsqlConnection(this.connectionString);
                connection.Open();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }


        private string connectionString { get; }


        /// <inheritdoc />
        public void ExecuteCommand(string sql, List<DbParameter> parameters) {
            var cmd = new NpgsqlCommand(sql) {
                Connection = connection,
                CommandType = CommandType.Text
            };
            parameters.ForEach(param => cmd.Parameters.Add(param));
            reader = cmd.ExecuteReader();
        }


        /// <inheritdoc />
        public long? ExecuteNonQueryCommand(string sql, List<DbParameter> parameters) {
            var cmd = new NpgsqlCommand(sql) {
                Connection = connection,
                CommandType = CommandType.Text
            };
            parameters.ForEach(param => cmd.Parameters.Add(param));
            return (long?) cmd.ExecuteScalar();
        }


        /// <inheritdoc />
        public T GetValue<T>(int columnId) {
            if (reader == null) throw new Exception("DbReader non initialisé");
            return reader.IsDBNull(columnId) ? default : reader.GetFieldValue<T>(columnId);
        }


        /// <inheritdoc />
        public bool Read() {
            if (reader == null) throw new Exception("DbReader non initialisé");
            return reader.Read();
        }


        /// <inheritdoc />
        public void EndCommand() {
            reader?.Close();
        }


        public void CloseHandler() {
            if (reader != null) {
                if (!reader.IsClosed) reader.Close();
                reader.Dispose();
            }

            if (connection?.State != ConnectionState.Closed) connection?.Close();
        }

    }

}