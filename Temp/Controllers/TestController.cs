using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arch.EntityFrameworkCore.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Temp.Data;

namespace Temp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<TestController> logger;
        public TestController(IUnitOfWork unitOfWork, ILogger<TestController> logger)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<Blog>> Get()
        {
            var item = await unitOfWork.GetRepository<Blog>().GetFirstOrDefaultAsync(predicate: x => x.Id == 1);
            logger.LogInformation(JsonConvert.SerializeObject(item));
            //var list = await unitOfWork.GetRepository<Blog>().GetPagedListAsync(x => (x.Id > 0 && x.Title.Contains("1")));
            //logger.LogInformation(JsonConvert.SerializeObject(list));
            //unitOfWork.GetRepository<LoginToken>().ChangeTable("logintoken_202007");
            //unitOfWork.ChangeDatabase("uow");
            var repository = unitOfWork.GetRepository<LoginToken>();
            repository.ChangeTable("logintoken_202007");//此组件 第一次调用此方法时要报错

            var res = repository.GetFirstOrDefault(predicate: x => x.KID == 1);

            return new List<Blog>() { item};
        }
    }
}
