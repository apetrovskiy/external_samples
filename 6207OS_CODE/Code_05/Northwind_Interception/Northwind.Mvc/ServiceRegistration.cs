using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Web.Mvc.Filter;
using Ninject.Web.Mvc.FilterBindingSyntax;
using Northwind.Core;
using Northwind.Interception;
using Northwind.SqlDataAccess;
using log4net;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Interception.Infrastructure.Language;

namespace Northwind.Mvc
{
    public class ServiceRegistration : NinjectModule
    {
        public override void Load()
        {

          Bind<ILog>().ToMethod(GetLogger);

            Kernel.BindFilter<LogFilter>(FilterScope.Action, 0)
                .WhenActionMethodHas<LogAttribute>()
                .WithConstructorArgumentFromActionAttribute<LogAttribute>(
                    "logLevel",
                    attribute => attribute.LogLevel);            
            
            Kernel.Bind(x => x.FromAssembliesMatching("Northwind.*")
                                    .SelectAllClasses()
                                    .BindAllInterfaces()
                                    .Configure(b =>
                                        b.Intercept()
                                        .With<ExceptionInterceptor>()
                                    ));

            //Kernel.Bind(x => x.FromAssembliesMatching("Northwind.*")
            //            .SelectAllClasses()
            //            .BindAllInterfaces()
            //            .Configure(b =>
            //                            {
            //                                b.Intercept()
            //                                .With<ExceptionInterceptor>()
            //                                .InOrder(1);

            //                                b.Intercept()
            //                                .With<LoggerInterceptor>()
            //                                .InOrder(2);
            //                            }
            //                        ));

  

            //Kernel.InterceptReplace<SqlCustomerRepository>(
            //    r => r.GetAll(),
            //    invocation =>
            //        {
            //            const string cacheKey = "customers";
            //            if (HttpRuntime.Cache[cacheKey] == null)
            //            {
            //                invocation.Proceed();
            //                if (invocation.ReturnValue != null)
            //                {
            //                    HttpRuntime.Cache[cacheKey] = invocation.ReturnValue;
            //                }
            //            }
            //            else
            //            {
            //                invocation.ReturnValue = HttpRuntime.Cache[cacheKey];
            //            }
            //        });

            //var repositoryType = Type.GetType(
            //    "Northwind.SqlDataAccess.SqlCustomerRepository, Northwind.SqlDataAccess");
            //Kernel.AddMethodInterceptor(repositoryType.GetMethod("GetAll"),
            //                            invocation =>
            //                                {
            //                                    const string cacheKey = "customers";
            //                                    if (HttpRuntime.Cache[cacheKey] == null)
            //                                    {
            //                                        invocation.Proceed();
            //                                        HttpRuntime.Cache[cacheKey] = invocation.ReturnValue;
            //                                    }
            //                                    else
            //                                    {
            //                                        invocation.ReturnValue = HttpRuntime.Cache[cacheKey];
            //                                    }
            //                                });
        }

        private static ILog GetLogger(IContext ctx)
        {
            var filterContext = ctx.Request.ParentRequest.Parameters
                            .OfType<FilterContextParameter>().SingleOrDefault();
            return LogManager.GetLogger(filterContext == null ?
                ctx.Request.Target.Member.DeclaringType :
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerType);
        }
    }
}