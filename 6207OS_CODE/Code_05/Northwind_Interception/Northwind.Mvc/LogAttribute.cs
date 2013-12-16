using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Northwind.Mvc
{
public class LogAttribute : Attribute
{
    public string LogLevel { get; set; }
}
}