using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Fina.Core.Requests.Transactions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Transactions;

public partial class CreateTransactionPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; }
    public CreateTransactionRequest InputModel { get; set; } = new();
    public List<Category?> Categories { get; set; } = [];

    #endregion

    #region Services

    [Inject] 
    public ITransactionHandler Handler { get; set; } = null!;
    [Inject]
    public ICategoryHandler CategoryHandler { get; set; } = null!;
    [Inject] 
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] 
    public ISnackbar Snackbar { get; set; } = null!;

    #endregion

    #region Methods

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;

        try
        {
            var request = new GetAllCategoriesRequest();
            var result = await CategoryHandler.GetAllAsync(request);
            if (result.IsSuccess)
            {
                Categories = result.Data ?? [];
                InputModel.CategoryId = Categories.FirstOrDefault()?.Id ?? 0;
            }
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }
    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.CreateAsync(InputModel);
            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message, Severity.Success);
                NavigationManager.NavigateTo("/transactions");
            }
            else
            {
                Snackbar.Add(result.Message, Severity.Error);
            }
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion

}