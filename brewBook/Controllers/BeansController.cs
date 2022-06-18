using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using brewBook.Models;
using brewBook.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace brewBook.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
    public class BeansController : ControllerBase
    {
        private readonly BeansService _bs;

		public BeansController(BeansService bs)
		{
			_bs = bs;
		}

		[HttpGet]
		public ActionResult<List<Bean>> Get()
		{
			try
			{
				List<Bean> beans = _bs.Get();
				return Ok(beans);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id}")]
		public ActionResult<Bean> Get(int id)
		{
			try
			{
				Bean bean = _bs.Get(id);
				return Ok(bean);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<Bean>> Create([FromBody] Bean bean)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				bean.CreatorId = userInfo.Id;
				Bean newBean = _bs.Create(bean);
				newBean.CreatorId = userInfo.Id;
				return Ok(newBean);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<ActionResult<String>> Delete(int id) {
			try {
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				_bs.Delete(id, userInfo.Id);
				return Ok("Bean deleted");
			} catch (Exception e) {
				return BadRequest(e.Message);
			}
		}
	}
}

