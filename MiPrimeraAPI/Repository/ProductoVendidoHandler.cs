using MiPrimeraAPI.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiPrimeraAPI.Repository
{
    public static class ProductoVendidoHandler
    {
        public const string ConnectionString = "Server=LAPTOP-GHLLNC5Q\\SQLEXPRESS;Database=SistemaGestion;Trusted_Connection=True";
        public static List<ProductoVendido> GetProductoVendido()
        {
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                var queryProductoVendido = "SELECT * FROM ProductoVendido";
                using (SqlCommand sqlCommand = new SqlCommand(queryProductoVendido, sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();
                                productoVendido.Id = Convert.ToInt32(dataReader["Id"]);
                                productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

                                productosVendidos.Add(productoVendido);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return productosVendidos;
        }
    }
}
