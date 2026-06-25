namespace Ecommerce.Application.ViewModels
{
    public class CarrinhoViewModel
    {
        public Guid Id { get; set; }
        public List<ItemCarrinhoViewModel> Itens { get; set; } = new();
        public decimal Total => Itens.Sum(x => x.SubTotal);
    }
}
