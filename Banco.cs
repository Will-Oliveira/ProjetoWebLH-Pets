using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Projeto_Web_Lh_Pets_versão_1
{
    class Cliente
    {
        public string? CpfCnpj { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string RgIe { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorImposto { get; set; }
        public decimal Total { get; set; }
    }

    class Banco
    {
        private List<Cliente> lista = new List<Cliente>();

        public List<Cliente> GetLista()
        {
            return lista;
        }

        public Banco()
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                {
                    DataSource = "SHADOW\\SQLEXPRESS", // Nome do servidor
                    InitialCatalog = "vendas",         // Nome do banco de dados
                    IntegratedSecurity = true,          // Usa as credenciais do Windows para autenticação
                };

                using (SqlConnection conexao = new SqlConnection(builder.ConnectionString))
                {
                    String sql = "SELECT * FROM tblclientes";
                    using (SqlCommand comando = new SqlCommand(sql, conexao))
                    {
                        conexao.Open();
                        using (SqlDataReader tabela = comando.ExecuteReader())
                        {
                            while (tabela.Read())
                            {
                                lista.Add(new Cliente
                                {
                                    CpfCnpj = tabela["cpf_cnpj"].ToString(),
                                    Nome = tabela["nome"].ToString(),
                                    Endereco = tabela["endereco"].ToString(),
                                    RgIe = tabela["rg_ie"].ToString(),
                                    Tipo = tabela["tipo"].ToString(),
                                    Valor = Convert.ToDecimal(tabela["valor"]),
                                    ValorImposto = Convert.ToDecimal(tabela["valor_imposto"]),
                                    Total = Convert.ToDecimal(tabela["total"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
