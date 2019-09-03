using System.Collections.Generic;
using System.Data.Common;
using coworkdomain.Cowork;
using coworkdomain.Cowork.Interfaces;
using coworkpersistence.Datamappers;
using coworkpersistence.DomainBuilders;
using coworkpersistence.Handlers;
using Npgsql;

namespace coworkpersistence.Repositories {

    public class PlaceRepository : IPlaceRepository {

        private readonly SqlDataMapper<Place> datamapper;


        public PlaceRepository(string connectionString) {
            datamapper = new SqlDataMapper<Place>(SqlDbType.Postgresql, connectionString, new PlaceBuilder());
        }


        public List<Place> GetAll() {
            const string sql = "SELECT * FROM public.\"Place\"";
            return datamapper.MultiItemCommand(sql, new List<DbParameter>());
        }


        public Place GetById(long id) {
            const string sql = "SELECT * FROM public.\"Place\" WHERE  \"Id\" = @p";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("p", id)
            };
            return datamapper.OneItemCommand(sql, parameters);
        }


        public Place GetByName(string name) {
            const string sql = "SELECT * FROM public.\"Place\" WHERE  \"Name\" = @p";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("p", name)
            };
            return datamapper.OneItemCommand(sql, parameters);
        }


        public long Update(Place place) {
            const string sql =
                "UPDATE public.\"Place\" SET \"Name\" = @name, \"HighBandwidthWifi\" = @wifi, \"MembersOnlyArea\" = @membersOnlyArea, \"UnlimitedBeverages\" = @unlimitedBeverages, \"CosyRoomAmount\" = @cosyRoomAmount, \"LaptopAmount\"= @laptopAmount, \"PrinterAmount\"= @printerAmount WHERE \"Id\" = @id RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", place.Id),
                new NpgsqlParameter("name", place.Name),
                new NpgsqlParameter("wifi", place.HighBandwidthWifi),
                new NpgsqlParameter("membersOnlyArea", place.MembersOnlyArea),
                new NpgsqlParameter("unlimitedBeverages", place.UnlimitedBeverages),
                new NpgsqlParameter("cosyRoomAmount", place.CosyRoomAmount),
                new NpgsqlParameter("printerAmount", place.PrinterAmount),
                new NpgsqlParameter("laptopAmount", place.LaptopAmount)
            };
            return datamapper.NoQueryCommand(sql, parameters);
        }


        public bool DeleteById(long id) {
            const string sql = "DELETE FROM public.\"Place\" WHERE \"Id\"=@id RETURNING  \"Id\"";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", id)
            };
            return datamapper.NoQueryCommand(sql, parameters) > -1;
        }


        public bool DeleteByName(string name) {
            const string sql = "DELETE FROM public.\"Place\" WHERE \"Name\"=@name RETURNING  \"Id\"";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("name", name)
            };
            return datamapper.NoQueryCommand(sql, parameters) > -1;
        }


        public long Create(Place place) {
            const string sql =
                "INSERT INTO public.\"Place\" (\"Id\", \"Name\", \"HighBandwidthWifi\", \"MembersOnlyArea\", \"UnlimitedBeverages\", \"CosyRoomAmount\", \"PrinterAmount\", \"LaptopAmount\") VALUES (DEFAULT, @name, @wifi, @membersOnlyArea, @unlimitedBeverage, @cosyRoomAmount, @printerAmount, @laptopAmount) RETURNING \"Id\";";
            var parameters = new List<DbParameter> {
                new NpgsqlParameter("id", place.Id),
                new NpgsqlParameter("name", place.Name),
                new NpgsqlParameter("wifi", place.HighBandwidthWifi),
                new NpgsqlParameter("membersOnlyArea", place.MembersOnlyArea),
                new NpgsqlParameter("unlimitedBeverage", place.UnlimitedBeverages),
                new NpgsqlParameter("cosyRoomAmount", place.CosyRoomAmount),
                new NpgsqlParameter("printerAmount", place.PrinterAmount),
                new NpgsqlParameter("laptopAmount", place.LaptopAmount)
            };
            return datamapper.NoQueryCommand(sql, parameters);
        }

    }

}