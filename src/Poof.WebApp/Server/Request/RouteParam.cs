using Microsoft.AspNetCore.Mvc;
using System;
using Yaapii.Atoms;
using Yaapii.Atoms.Error;

namespace Poof.Web.Server.Request
{
    /// <summary>
    /// A route parameter from the current route.
    /// </summary>
    public sealed class RouteParam : IScalar<string>
    {
        private readonly ControllerBase ctrl;
        private readonly string param;

        /// <summary>
        /// A route parameter from the current route.
        /// </summary>
        /// <param name="param">name of the parameter</param>
        /// <param name="controller">current controller</param>
        public RouteParam(string param, ControllerBase controller)
        {
            this.ctrl = controller;
            this.param = param;
        }

        /// <summary>
        /// The parameter.
        /// </summary>
        /// <returns>Requested parameter as string</returns>
        public string Value()
        {
            new FailPrecise(
                new FailWhen(!this.ctrl.RouteData.Values.ContainsKey(this.param)),
                new ArgumentException($"Missing parameter '{this.param}' in the route '{this.ctrl.Request.QueryString}'")
            ).Go();

            return this.ctrl.RouteData.Values[this.param].ToString();
        }
    }
}
