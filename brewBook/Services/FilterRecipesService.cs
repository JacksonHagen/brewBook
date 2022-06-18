using System;
using System.Collections.Generic;
using brewBook.Models;
using brewBook.Repositories;

namespace brewBook.Services
{
	public class FilterRecipesService
	{
		private readonly FilterRecipesRepository _repo;

		public FilterRecipesService(FilterRecipesRepository repo)
		{
			_repo = repo;
		}

		internal List<FilterRecipe> Get()
		{
			return _repo.Get();
		}

		internal FilterRecipe Get(int id)
		{
			FilterRecipe found = _repo.Get(id);
			if (found == null)
			{
				throw new Exception("Invalid Id");
			}
			return found;
		}

		internal FilterRecipe Create(FilterRecipe recipe)
		{
			FilterRecipe newRecipe = _repo.Create(recipe);
			return _repo.Get(newRecipe.Id);
		}

		internal FilterRecipe Edit(FilterRecipe updateData)
		{
			FilterRecipe original = Get(updateData.Id);
			IsOwner(original.CreatorId, updateData.CreatorId);
			original.BeanId = updateData.BeanId != 0 ? updateData.BeanId : original.BeanId;
			original.Title = updateData.Title ?? original.Title;
			original.Dose = updateData.Dose != 0 ? updateData.Dose : original.Dose;
			original.Temp = updateData.Temp != 0 ? updateData.Temp : original.Temp;
			original.Grinder = updateData.Grinder ?? original.Grinder;
			original.GrindSetting = updateData.GrindSetting ?? original.GrindSetting;
			original.Description = updateData.Description ?? original.Description;
			original.TimeInSeconds = updateData.TimeInSeconds != 0 ? updateData.TimeInSeconds : original.TimeInSeconds;
			original.TotalWeight = updateData.TotalWeight != 0 ? updateData.TotalWeight : original.TotalWeight;
			original.Brewer = updateData.Brewer ?? original.Brewer;

			_repo.Edit(original);
			return original;
		}

		internal void Delete(int id, string userId)
		{
			FilterRecipe target = Get(id);
			IsOwner(target.CreatorId, userId);
			_repo.Delete(id);
		}

		private void IsOwner(string id1, string id2)
		{
			if (id1 != id2)
			{
				throw new Exception("You are not the owner of this recipe");
			}
		}
	}
}