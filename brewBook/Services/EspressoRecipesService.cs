using System;
using System.Collections.Generic;
using brewBook.Models;
using brewBook.Repositories;

namespace brewBook.Services
{
	public class EspressoRecipesService
	{
		private readonly EspressoRecipesRepository _repo;

		public EspressoRecipesService(EspressoRecipesRepository repo)
		{
			_repo = repo;
		}

		internal List<EspressoRecipe> Get()
		{
			return _repo.Get();
		}

		internal EspressoRecipe Get(int id)
		{
			EspressoRecipe found = _repo.Get(id);
			if (found == null)
			{
				throw new Exception("Could not find recipe with id: " + id);
			}
			return found;
		}

		internal EspressoRecipe Create(EspressoRecipe recipe)
		{
			return _repo.Create(recipe);
		}

		internal EspressoRecipe Edit(EspressoRecipe updateData)
		{
			EspressoRecipe original = _repo.Get(updateData.Id);
			IsRecipeOwner(original.CreatorId, updateData.CreatorId);
			original.Title = updateData.Title ?? original.Title;
			original.Grinder = updateData.Grinder ?? original.Grinder;
			original.GrindSetting = updateData.GrindSetting ?? original.GrindSetting;
			original.Description = updateData.Description ?? original.Description;
			original.Machine = updateData.Machine ?? original.Machine;

			original.Dose = updateData.Dose != 0 ? updateData.Dose : original.Dose;
			original.Temp = updateData.Temp != 0 ? updateData.Temp : original.Temp;
			original.PreinfusionTime = updateData.PreinfusionTime != 0 ? updateData.PreinfusionTime : original.PreinfusionTime;
			original.ExtractionTime = updateData.ExtractionTime != 0 ? updateData.ExtractionTime : original.ExtractionTime;
			_repo.Edit(original);
			return original;
		}

		internal void Delete(int recipeId, string userId)
		{
			EspressoRecipe target = _repo.Get(recipeId);
			IsRecipeOwner(target.CreatorId, userId);
			_repo.Delete(recipeId);
		}

		private void IsRecipeOwner(string id1, string id2)
		{
			if (id1 != id2)
			{
				throw new Exception("You are not the owner of this recipe");
			}
		}
	}
}