using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using ShelfLabelPrintAPI.Models;

using System.Text;

namespace ShelfLabelPrintAPI.Services
{
    public class ProductDtlService
    {
        private readonly string _connectionString;
        private string connectionString;
        public ProductDtlService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ProductDtl GetProduct(string barcode, string loc)
        {
            ProductDtl Product = new ProductDtl();
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT 
                                                            SU_DESCRIPTION,
                                                            SU_DESCRIPTION_AR,
                                                            SELLING_PRICE
                                                        FROM
                                                            GRAND_PRD_MASTER_FULL_NEW A,
                                                            POS_CONTROL B
                                                        WHERE
                                                            A.PRODUCT_CODE = B.GOLD_CODE
                                                            AND A.SU = B.SU
                                                            AND A.BARCODE = '" + barcode + @"'
                                                            AND STORE_ID = '" + loc + @"'";
                using (OracleCommand cmd = new OracleCommand(query, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Product.Su_desc = reader["SU_DESCRIPTION"].ToString();
                                Product.Su_desc_ar = reader["SU_DESCRIPTION_AR"].ToString();
                                Product.Price = reader["SELLING_PRICE"].ToString();
                            }
                        }
                    }
                }
                connection.Close();
            }
            return Product;
        }

        public IEnumerable<ProductDtl> GetProducts()
        {
            List<ProductDtl> products = new List<ProductDtl>();
            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT DISTINCT SU_DESCRIPTION,
                                                            SU_DESCRIPTION_AR,
                                                            SELLING_PRICE
                                                        FROM
                                                            GRAND_PRD_MASTER_FULL_NEW A,
                                                            POS_CONTROL B
                                                        WHERE
                                                            A.PRODUCT_CODE = B.GOLD_CODE
                                                            AND A.SU = B.SU
                                                            AND ROWNUM<500";
                using (OracleCommand cmd = new OracleCommand(query, connection))
                {
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductDtl product = new ProductDtl();
                            product.Su_desc = reader["SU_DESCRIPTION"].ToString();
                            product.Su_desc_ar = reader["SU_DESCRIPTION_AR"].ToString();
                            product.Price = reader["SELLING_PRICE"].ToString();
                            products.Add(product);
                        }
                    }
                }
                connection.Close();
            }
            return products;
        }

    }    
}
