using Microsoft.AspNetCore.Components;
using MudBlazor;
using ProductAdminPanel.DAL.Models;
using ProductAdminPanel.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ProductAdminPanel.Components
{
    public partial class SupplierForm
    {
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; } = default!;
        [Inject] private ISupplierService SupplierService { get; set; } = default!;

        [Parameter] public Supplier? ExistingSupplier { get; set; }

        private Supplier _supplier = new();
        private MudForm? _formRef;
        private bool _isEdit => ExistingSupplier != null;

        protected override void OnInitialized()
        {
            if (_isEdit && ExistingSupplier is not null)
            {
                _supplier = new Supplier
                {
                    Id = ExistingSupplier.Id,
                    Name = ExistingSupplier.Name,
                    ContactEmail = ExistingSupplier.ContactEmail,
                    ContactNumber = ExistingSupplier.ContactNumber,
                    Website = ExistingSupplier.Website
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
                await SupplierService.UpdateAsync(_supplier);
            else
                await SupplierService.AddAsync(_supplier);

            MudDialog.Close(DialogResult.Ok(true));
        }

        private void Cancel() => MudDialog.Cancel();

        private void OnNameChanged(string value) => _supplier.Name = value;
        private void OnEmailChanged(string value) => _supplier.ContactEmail = value;
        private void OnPhoneChanged(string value) => _supplier.ContactNumber = value;
        private void OnWebsiteChanged(string value) => _supplier.Website = value;

        private string? ValidateEmail(string email) =>
            new EmailAddressAttribute().IsValid(email) ? null : "Invalid email address";
    }
}
