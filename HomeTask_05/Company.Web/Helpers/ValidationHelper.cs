using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Company.Web.Helpers;

public class ValidationHelper
{
    public static bool IsValidModel(ModelStateDictionary model, EditAccess level)
    {
        return level switch
        {
            EditAccess.Basic => CheckStates(model, "FullName", "BirthDate", "DepartmentId"),
            EditAccess.Admin => CheckStates(model, "FullName", "BirthDate", "DepartmentId", "Email", "RoleId"),
            EditAccess.Full => CheckStates(model, "FullName", "BirthDate", "DepartmentId", "Email", "RoleId", "NewPassword"),
            _ => false
        };
    }

    private static bool CheckStates(ModelStateDictionary model, params string[] Keys) => 
        Keys.ToList().TrueForAll(x => model[x].ValidationState == ModelValidationState.Valid);
}
