using System;
using System.Collections.Generic;
using System.Linq;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            CadastraPedido();
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

        private static void ConsultaDados()
        {
            using var db = new Data.ApplicationContext();
            var consultapPorMetodo = db.Clientes.Where(p => p.Id > 0).ToList();
            //asnotrack == consulta direto no banco

            foreach (var cliente in consultapPorMetodo)
            {
                Console.WriteLine($"Consultando Cliente: {cliente.Id}");
                db.Clientes.Find(cliente.Id);
            }
        }

        private static void CadastraPedido()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.FirstOrDefault(); //FirstOrDefault pega o primeiro registro encontrado
            var Produto = db.Produtos.FirstOrDefault();

            var Pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = Produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor= 10,
                    }
                }
            };

            db.Pedido.Add(Pedido);
            db.SaveChanges();
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedido = db.Pedido
            .Include(p => p.Itens)
            .ThenInclude(p => p.Produto)
            .ToList();
            Console.WriteLine(pedido.Count);
        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(1);
            cliente.Nome = "Cliente Alterado Passo 1";

            //db.Clientes.Update(cliente); sen essa linha só vai alterar oq foi feito alteração
            db.SaveChanges();
        }

        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(2);

            //db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();

        }

    }
}
