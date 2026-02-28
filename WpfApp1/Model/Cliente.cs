using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }

        // Relação 1:N com Conta
        public ICollection<Conta> Contas { get; set; } = new List<Conta>();

        // Construtor para facilitar a criação (Clean Code)
        public Cliente(string nome, string cpf, string endereco)
        {
            Nome = nome;
            CPF = cpf;
            Endereco = endereco;
        }

        // Construtor sem parâmetros é necessário para o Entity Framework Core (EF Core)
        public Cliente() { }

    }
}
