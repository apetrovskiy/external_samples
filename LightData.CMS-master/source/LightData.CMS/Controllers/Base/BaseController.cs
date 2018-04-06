using LightData.Auth.Controllers;
using LightData.Auth.Helper;
using LightData.CMS.Modules.Library;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LightData.CMS.Controllers.Base
{
    public class BaseController : SharedController
    {
        private EntityWorker.Core.InterFace.IRepository _repository;

        protected EntityWorker.Core.InterFace.IRepository Repository
        {
            get
            {
                if (_repository == null)
                    _repository = new LightData.CMS.Modules.Repository.Repository();
                return _repository;
            }
        }

        [HttpPost]
        public ExternalActionResult GetActiveCountries()
        {
            return Repository.Get<Country>().Where(x => x.Visible == true).Execute().ToJsonResult();
        }

        [HttpPost]
        public async Task<ExternalActionResult> GetCountries(string filter)
        {
            var countries = await Repository.Get<Country>().Where(x => string.IsNullOrEmpty(filter) || filter.Contains(x.Name)).ExecuteAsync();

            return await countries.ToJsonResultAsync();
        }

        [HttpPost]
        public void SaveCountry(Country country)
        {
            Repository.Save(country);
        }
    }
}