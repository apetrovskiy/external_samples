using EntityWorker.Core.Helper;
using LightData.Auth.Helper;
using LightData.CMS.Modules.Library;
using LightData.CMS.Modules.Repository;
using System.Collections;
using System.Text;
using System.Web.Mvc;
using static LightData.CMS.Modules.Helper.EnumHelper;

namespace LightDataTable.Site.Helpers
{
    public static class HtmlCustomHelper
    {
        public static MvcHtmlString Tag(this HtmlHelper htmlHelper, Tags tag, dynamic attributes = null)
        {
            var str = new StringBuilder();
            if (tag == Tags.TopMenu)
                return new MvcHtmlString(GetTopMenu());
            str.Append("<tag title='" + tag.ToString() + "' type='" + tag.ToString() + "'");
            string value = null;
            if (attributes != null)
            {
                var dic = attributes as IDictionary;
                foreach (var key in dic.Keys)
                {
                    if (tag == Tags.HtmlContent && string.Equals(key.ToString(), "value", System.StringComparison.CurrentCultureIgnoreCase))
                        value = dic[key]?.ConvertValue<string>();
                    str.Append(string.Format(" {0}='{1}'", key, dic[key]));
                }

            }
            if (value != null)
                str.Append(">" + value + " </tag>");
            else
                str.Append("> </tag>");
            return MvcHtmlString.Create(str.ToString());
        }


        public static string GetTopMenu()
        {
            var data = new Repository().Get<Menus>().Where(x => x.Publish && !x.ParentId.HasValue).LoadChildren().Execute().ToJson();
            var str = new StringBuilder("<script> $('#cssmenu').horizontalMenu({");
            str.Append("datasource:");
            str.Append(data);
            str.Append("});</script>");
            return str.ToString();

        }
    }
}