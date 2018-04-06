using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using LightData.Auth.Helper;
using LightData.CMS.Controllers.Base;
using LightData.CMS.Modules.Library;

namespace LightData.CMS.Controllers
{
    public class ArticleController : BaseController
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ExternalActionResult> Get(int pageNr, string value)
        {
            var articles = await Repository.Get<Article>().Where(x => value == "" || x.ArticleName.Contains(value))
                .Skip((pageNr - 1) * SearchResultValue).Take(SearchResultValue).OrderBy(x => x.ArticleName)
                .LoadChildren().ExecuteAsync();

            return await articles.ToJsonResultAsync();
        }

        [HttpPost]
        public void Save(Article item)
        {
            Repository.Save(item);
            Repository.SaveChanges();
        }

        [HttpPost]
        public void Delete(List<Guid> items)
        {
            items.ForEach(a => Repository.Get<Article>().LoadChildren().Where(x => x.Id == a).Remove());
            Repository.SaveChanges();
        }
   
    }
}