using System;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            InserirDados();
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descrição = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            db.Produtos.Add(produto);
            //db.Set<Produto>().Add(produto);
            //não recomendado
            //db.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            //db.Add(produto);

            //salvando no banco
            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
        }
    }
}
