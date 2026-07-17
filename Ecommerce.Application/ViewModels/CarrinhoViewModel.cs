namespace Ecommerce.Application.ViewModels
{
    public class CarrinhoViewModel
    {
        public Guid Id { get; set; }
        public List<ItemCarrinhoViewModel> Itens { get; set; } = new();
        public int QuantidadeTotal => Itens.Sum(item => item.Quantidade);
        public decimal Subtotal => Itens.Sum(item => item.SubTotal);
        public decimal Frete { get; set; } = 0;
        public decimal Total => Subtotal + Frete;
        public bool EstaVazio => !Itens.Any();
    }
}
