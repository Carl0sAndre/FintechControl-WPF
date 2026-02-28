using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Persistence;


namespace WpfApp1
{
    public class Poupanca : Conta
    {
        public decimal TaxaJuros { get; set; }
        public void AplicarRendimento(FintechPooDbContext context)
        {
            // SQL Puro: Atualiza o saldo de todas as poupanças adicionando 1%
            // A coluna Discriminator garante que só pegue Poupança
            string sql = "UPDATE Contas SET Saldo = Saldo * 1.01 WHERE TaxaJuros IS NOT NULL";

            // Executa direto no banco. O EF gerencia a conexão sozinho.
            context.Database.ExecuteSqlRaw(sql);
        }
    }
}
