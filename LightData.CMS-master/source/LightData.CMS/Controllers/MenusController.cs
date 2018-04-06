using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using EntityWorker.Core.Helper;
using LightData.Auth.Helper;
using LightData.CMS.Controllers.Base;
using LightData.CMS.Modules.Library;

namespace LightData.CMS.Controllers
{
    public class MenusController : BaseController
    {
        // GET: Menus
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ExternalActionResult> GetMenus()
        {
            var menus = await Repository.Get<Menus>().LoadChildren().Where(x => x.ParentId == null).ExecuteAsync();
            return await menus.ToJsonResultAsync();
        }

        [HttpPost]
        public async Task<ExternalActionResult> GetAutoFillData(string value)
        {
            var menus = new List<Menus>();
            var id = value?.ConvertValue<Guid?>();
            if (id.HasValue)
                menus = await Repository.Get<Menus>().Where(x => x.Id == id).ExecuteAsync();
            else
                menus = await Repository.Get<Menus>().LoadChildren()
                    .Where(x => x.DisplayName.Contains(value) || x.Children.Any(a => a.DisplayName.Contains(value)))
                    .ExecuteAsync();
            return await menus.ToJsonResultAsync();

        }

        [HttpPost]
        public void Save(Menus item)
        {
            Repository.Save(item);
            Repository.SaveChanges();
        }

        [HttpPost]
        public void Delete(Menus item)
        {
            Repository.Delete(item);
            Repository.SaveChanges();
        }

    }
}