using ADV.Entity;
using ADV.Models;
using ADV.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace ADV.Controllers
{
    /// <summary>
    /// 渠道信息
    /// </summary>
    public class ChannelController : ApiController
    {
        /// <summary>
        /// 提交复制信息
        /// </summary>
        /// <param name="channelid"></param>
        /// <param name="wxname"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<MvcAjaxResponse> Push(string channelid, string wxname)
        {
            var request = Request.Content;
            /*
             * 需要根据IP过滤请求次数
             * 同一IP在复制一次有效？还是一天？
             */
            adv_c_ChanelService acs = new adv_c_ChanelService();
            var result = acs.PushData(channelid, wxname);
            return Json(new MvcAjaxResponse { Success = true, Result = result });
        }
    }
}
