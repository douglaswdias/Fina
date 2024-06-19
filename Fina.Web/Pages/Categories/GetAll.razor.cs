using Fina.Core.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Categories;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Fina.Web.Pages.Categories;

public partial class GetAllCategoriesPage : ComponentBase
{
    #region Properties

    public bool IsBusy { get; set; } = false;
    public List<Category> Categories { get; set; } = [];
    
    #endregion

    #region Services

    [Inject] 
    public ICategoryHandler Handler { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    [Inject]
    public ISnackbar Snackbar { get; set; } = null!;
    [Inject]
    public IDialogService Dialog { get; set; } = null!;

    #endregion
    
    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        IsBusy = true;
        try
        {
            var request = new GetAllCategoriesRequest();
            var result = await Handler.GetAllAsync(request);
            if (result.IsSuccess)
                Categories = result.Data ?? [];
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

    #region Methods

    public async void OnClickDeleteButtonAsync(long id, string title)
    {
        var result = await Dialog.ShowMessageBox("ATENÇÃO", $"Deseja Excluir a Categoria '{title}'?", yesText: "Excluir", cancelText: "Cancelar");
        if (result is true)
            await OnDeleteAsync(id, title);
        
        StateHasChanged();
    }

    public async Task OnDeleteAsync(long id, string title)
    {
        try
        {
            var request = new DeleteCategoryRequest
            {
                Id = id
            };
            await Handler.DeleteAsync(request);
            Categories.RemoveAll(x => x.Id == id);
            Snackbar.Add($"Categoria '{title}' removida", Severity.Info);
        }
        catch (Exception e)
        {
            Snackbar.Add(e.Message, Severity.Error);
        }
    }

    #endregion
    
}