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
	public class EspressoRecipesController : ControllerBase
	{
		private readonly EspressoRecipesService _ers;

		public EspressoRecipesController(EspressoRecipesService ers)
		{
			_ers = ers;
		}

		[HttpGet]
		public ActionResult<List<EspressoRecipe>> Get()
		{
			try
			{
				List<EspressoRecipe> recipes = _ers.Get();
				return Ok(recipes);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("{id}")]
		public ActionResult<EspressoRecipe> Get(int id)
		{
			try
			{
				EspressoRecipe recipe = _ers.Get(id);
				return Ok(recipe);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<EspressoRecipe>> Create([FromBody] EspressoRecipe recipe)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				recipe.CreatorId = userInfo.Id;
				EspressoRecipe newRecipe = _ers.Create(recipe);
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
		public async Task<ActionResult<EspressoRecipe>> Edit([FromBody] EspressoRecipe updateData, int id)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				updateData.CreatorId = userInfo.Id;
				updateData.Id = id;
				EspressoRecipe updatedRecipe = _ers.Edit(updateData);
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
		public async Task<ActionResult<String>> Delete(int id)
		{
			try
			{
				Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
				_ers.Delete(id, userInfo.Id);
				return Ok("Deleted");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}