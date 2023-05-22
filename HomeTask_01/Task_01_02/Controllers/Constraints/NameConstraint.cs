using System.Text.RegularExpressions;

namespace Task_01.Controllers.Constraints;

public class NameConstraint : IRouteConstraint
{
    static readonly Regex NameRegex = new(@"^([A-Za-z]+)(['|-]?[A-Za-z]+)*$");
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        return NameRegex.IsMatch(values[routeKey] as string ?? "");
    }
}
