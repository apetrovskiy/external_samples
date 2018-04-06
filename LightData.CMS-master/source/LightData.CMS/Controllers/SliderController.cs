using LightData.Auth.Helper;
using LightData.CMS.Controllers.Base;
using LightData.CMS.Modules.Library;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using System;

namespace LightData.CMS.Controllers
{
    public class SliderController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ExternalActionResult> Get()
        {
            var sliderCollection = await Repository.Get<SliderCollection>().LoadChildren().IgnoreChildren(x =>
            x.Sliders.Select(a => a.File.Folder),
            x => x.Sliders.Select(a => a.File.Slider),
            x => x.Sliders.Select(a => a.SliderCollection)).ExecuteAsync();
            return await sliderCollection.ToJsonResultAsync();
        }

        [HttpPost]
        public void Save(SliderCollection item)
        {
            Repository.Save(item);
            Repository.SaveChanges();
        }

        [HttpPost]
        public void Delete(Guid itemId)
        {
            Repository.Get<SliderCollection>().Where(x => x.Id == itemId).IgnoreChildren(x => x.Sliders.Select(a => a.File), x => x.Sliders.Select(a => a.File.Slider), x => x.Sliders.Select(a => a.SliderCollection)).LoadChildren().Remove().SaveChanges();

        }

        [HttpPost]
        public void DeleteSlider(Guid itemId)
        {
            Repository.Get<Slider>().Where(x => x.Id == itemId).Remove().SaveChanges();
        }
    }
}