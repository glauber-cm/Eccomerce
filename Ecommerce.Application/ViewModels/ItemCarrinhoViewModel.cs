namespace Ecommerce.Application.ViewModels
{
    public class ItemCarrinhoViewModel
    {
        public Guid Id { get; set; }
        public string ProdutoNome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal SubTotal => Quantidade * PrecoUnitario; 

    }
}
