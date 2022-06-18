using System;
using System.Collections.Generic;
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
	public class FilterRecipesController : ControllerBase
	{
		private readonly FilterRecipesService _frs;

		public FilterRecipesController(FilterRecipesService frs)
		{
			_frs = frs;
		}

		[HttpGet]
		public ActionResult<List<FilterRecipe>> Get()
		{
			try
			{
				List<FilterRecipe> recipes = _frs.Get();
				return Ok(recipes);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id}")]
		public ActionResult<FilterRecipe> Get(int id)
		{
			try
			{
				FilterRecipe recipe = _frs.Get(id);
				return Ok(recipe);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<FilterRecipe>> Create([FromBody] FilterRecipe recipe)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				recipe.CreatorId = userInfo.Id;
				FilterRecipe newRecipe = _frs.Create(recipe);
				newRecipe.Creator = userInfo;
				return Ok(newRecipe);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	
		[HttpPut("{id}")]
		[Authorize]
		public async Task<ActionResult<FilterRecipe>> Edit([FromBody] FilterRecipe updateData, int id)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				updateData.CreatorId = userInfo.Id;
				updateData.Id = id;
				FilterRecipe updatedRecipe = _frs.Edit(updateData);
				updatedRecipe.Creator = userInfo;
				return Ok(updatedRecipe);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete("{id}")]
		[Authorize]
		public async Task<ActionResult<String>> Delete(int id) {
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				_frs.Delete(id, userInfo.Id);
				return Ok("Recipe deleted");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}