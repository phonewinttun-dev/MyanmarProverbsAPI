using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.WebSockets;

namespace MyanmarProverbsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProverbsController : ControllerBase
    {
        [HttpGet("ProverbsTitle")]
        public IActionResult GetProverbTitle()
        {
            var result = GetData();
            return Ok(result.Tbl_MMProverbsTitle);
        }

        [HttpGet("ProverbDetails")]
        public IActionResult GetProverbDetails()
        {
            var result = GetData();
            return Ok(result.Tbl_MMProverbs);
        }

        [HttpGet("TitleID/{TitleID}/ProverbID/{ProverbID}")]
        public IActionResult GetProverbs(int TitleID, int ProverbID)
        {
            var result = GetData();
            var item = result.Tbl_MMProverbs.FirstOrDefault(x => 
            x.TitleId == TitleID &&
            x.ProverbId == ProverbID);
            return Ok(item);
        }

        private ProverbsResponseModel GetData()
        {
            string fileName = "MyanmarProverbs.json";
            string json = System.IO.File.ReadAllText(fileName);
            var data = JsonConvert.DeserializeObject<ProverbsResponseModel>(json);
            return data;
        }
    }


    public class ProverbsResponseModel
    {
        public Tbl_Mmproverbstitle[] Tbl_MMProverbsTitle { get; set; }
        public Tbl_Mmproverbs[] Tbl_MMProverbs { get; set; }
    }

    public class Tbl_Mmproverbstitle
    {
        public int TitleId { get; set; }
        public string TitleName { get; set; }
    }

    public class Tbl_Mmproverbs
    {
        public int TitleId { get; set; }
        public int ProverbId { get; set; }
        public string ProverbName { get; set; }
        public string ProverbDesp { get; set; }
    }

}
