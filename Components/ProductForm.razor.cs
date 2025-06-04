using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductAdminPanel.DAL.Models;
using ProductAdminPanel.Services.Interfaces;

namespace ProductAdminPanel.Components
{
    public partial class ProductForm : ComponentBase
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [Inject] private IProductService ProductService { get; set; } = default!;
        [Inject] private ISupplierService SupplierService { get; set; } = default!;

        [Parameter] public Product? ExistingProduct { get; set; }

        private MudForm? _formRef;
        private Product _product = new();
        private List<Supplier> _suppliers = new();
        private bool _isEdit => ExistingProduct != null;

        protected override async Task OnInitializedAsync()
        {
            _suppliers = await SupplierService.GetAllAsync();

            if (_isEdit && ExistingProduct is not null)
            {
                _product = new Product
                {
                    Id = ExistingProduct.Id,
                    Name = ExistingProduct.Name,
                    SKU = ExistingProduct.SKU,
                    Description = ExistingProduct.Description,
                    Price = ExistingProduct.Price,
                    CostPrice = ExistingProduct.CostPrice,
                    StockQuantity = ExistingProduct.StockQuantity,
                    Status = ExistingProduct.Status,
                    LaunchDate = ExistingProduct.LaunchDate,
                    EndDate = ExistingProduct.EndDate,
                    SupplierId = ExistingProduct.SupplierId
                };
            }
        }

        private async Task Save()
        {
            if (_formRef is null)
                return;

            await _formRef.Validate();
            if (!_formRef.IsValid)
                return;

            if (_isEdit)
                await ProductService.UpdateAsync(_product);
            else
                await ProductService.AddAsync(_product);

            MudDialog.Close(DialogResult.Ok(true));
        }

        private void Cancel() => MudDialog.Cancel();
    }
}
