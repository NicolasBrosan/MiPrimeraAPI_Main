using MiPrimeraAPI.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiPrimeraAPI.Repository
{
    public static class ProductoHandler
    {
        public const string ConnectionString = "Server=LAPTOP-GHLLNC5Q\\SQLEXPRESS;Database=SistemaGestion;Trusted_Connection=True";
        public static  List<Producto> GetProductos()
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                var queryProducto = "SELECT * FROM Producto";
                using (SqlCommand sqlCommand = new SqlCommand(queryProducto, sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);

                                productos.Add(producto);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return productos;
        }
        public static bool UpdateProduct(Producto producto)
        {
            bool result = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUpdate = "UPDATE [SistemaGestion].[dbo].[Producto]" +
                    "SET Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario " +
                    "WHERE Id = @id ";

                SqlParameter descripciones = new SqlParameter("descripciones", System.Data.SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costo = new SqlParameter("costo", System.Data.SqlDbType.Decimal) { Value = producto.Costo };
                SqlParameter precioVenta = new SqlParameter("precioVenta", System.Data.SqlDbType.Decimal) { Value = producto.PrecioVenta };
                SqlParameter stock = new SqlParameter("stock", System.Data.SqlDbType.Int) { Value = producto.Stock };
                SqlParameter idUsuario = new SqlParameter("idUsuario", System.Data.SqlDbType.Int) { Value = producto.IdUsuario };
                SqlParameter id = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = producto.Id };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripciones);
                    sqlCommand.Parameters.Add(costo);
                    sqlCommand.Parameters.Add(precioVenta);
                    sqlCommand.Parameters.Add(stock);
                    sqlCommand.Parameters.Add(idUsuario);
                    sqlCommand.Parameters.Add(id);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        result = true;
                    }
                }
                sqlConnection.Close();
            }
            return result;
        }
        public static bool CreateProduct(Producto producto)
        {
            bool result = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) " +
                    "VALUES (@descripciones, @costo, @precioVenta, @stock, @idUsuario)";

                SqlParameter descripciones = new SqlParameter("Descripciones", System.Data.SqlDbType.VarChar) { Value = producto.Descripciones };
                SqlParameter costo = new SqlParameter("Costo", System.Data.SqlDbType.Decimal) { Value = producto.Costo };
                SqlParameter precioVenta = new SqlParameter("PrecioVenta", System.Data.SqlDbType.Decimal) { Value = producto.PrecioVenta };
                SqlParameter stock = new SqlParameter("Stock", System.Data.SqlDbType.Int) { Value = producto.Stock };
                SqlParameter idUsuario = new SqlParameter("IdUsuario", System.Data.SqlDbType.Int) { Value = producto.IdUsuario };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(descripciones);
                    sqlCommand.Parameters.Add(costo);
                    sqlCommand.Parameters.Add(precioVenta);
                    sqlCommand.Parameters.Add(stock);
                    sqlCommand.Parameters.Add(idUsuario);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        result = true;
                    }
                }
                sqlConnection.Close();
            }
            return result;
        }
        public static bool DeleteProduct(int id)
        {
            bool result = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                List<string> Querys = new List<string>();
                //--
                string queryDelete = "DELETE ProductoVendido FROM Producto AS P INNER JOIN ProductoVendido AS PV on P.ID = PV.IDPRODUCTO WHERE P.ID = @id";
                Querys.Add(queryDelete);
                //---
                string queryDelete2 = "DELETE FROM Producto WHERE Id = @id";
                Querys.Add(queryDelete2);
                //--
                sqlConnection.Open();
                foreach (var Query in Querys)
                {
                    SqlParameter sqlParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                    sqlParameter.Value = id;
                    using (SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(sqlParameter);
                        int numberOfRows = sqlCommand.ExecuteNonQuery();
                        if (numberOfRows > 0)
                        {
                            result = true;
                        }
                    }
                }
                sqlConnection.Close();
            }
            return result;

        }
    }
}
