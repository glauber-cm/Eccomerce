using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Application.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalProdutos { get; set; }
        public int TotalCategorias { get; set; }
        public int TotalPedidos { get; set; }
        public decimal FaturamentoTotal { get; set; }
    }
}
