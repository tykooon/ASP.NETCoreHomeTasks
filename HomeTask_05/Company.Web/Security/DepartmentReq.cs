using Microsoft.AspNetCore.Authorization;

namespace Company.Web.Security;

public class DepartmentReq : IAuthorizationRequirement
{
    public string Department { get; }

    public DepartmentReq(string department)
    {
        Department = department;
    }
}
