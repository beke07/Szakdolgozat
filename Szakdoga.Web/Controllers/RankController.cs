using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Szakdoga.BLL.ServiceInterfaces;

namespace Szakdoga.Web.Controllers
{
    public class RankController : Controller
    {
        private readonly IRankService rankService;

        public RankController(IRankService rankService)
        {
            this.rankService = rankService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRanks()
        {
            return Json(await rankService.GetRanksAsync());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRank(string name)
        {
            await rankService.CreateRankAsync(name);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRank(Guid id)
        {
            try
            {
                await rankService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(id);
            }
        }
    }
}
