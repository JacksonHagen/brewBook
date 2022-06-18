using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using brewBook.Models;
using Dapper;

namespace brewBook.Repositories
{
	public class EspressoRecipesRepository
	{
		private readonly IDbConnection _db;

		public EspressoRecipesRepository(IDbConnection db)
		{
			_db = db;
		}

		internal List<EspressoRecipe> Get()
		{
			string sql = @"
			SELECT
			er.*,
			b.*,
			a.*
			FROM espressorecipes er
			JOIN beans b ON er.beanId = b.id
			JOIN accounts a ON er.creatorId = a.id";
			return _db.Query<EspressoRecipe, Bean, Profile, EspressoRecipe>(sql, (recipe, bean, profile) =>
			{
				recipe.Creator = profile;
				recipe.Beans = bean;
				return recipe;
			}).ToList();
		}

		internal EspressoRecipe Get(int id)
		{
			string sql = @"
			SELECT
			er.*,
			b.*,
			a.*
			FROM espressorecipes er
			JOIN beans b ON er.beanId = b.id
			JOIN accounts a ON er.creatorId = a.id
			WHERE er.id = @id";
			return _db.Query<EspressoRecipe, Bean, Profile, EspressoRecipe>(sql, (recipe, bean, profile) =>
			{
				recipe.Creator = profile;
				recipe.Beans = bean;
				return recipe;
			}, new { id }).FirstOrDefault();
		}

		internal EspressoRecipe Create(EspressoRecipe recipe)
		{
			string sql = @"
			INSERT INTO espressorecipes
			(creatorId, title, dose, temp, grinder, grindSetting, description, machine, preinfusionTime, extractionTime, beanId, yield)
			VALUES
			(@CreatorId, @Title, @Dose, @Temp, @Grinder, @GrindSetting, @Description, @Machine, @PreinfusionTime, @ExtractionTime, @BeanId, @Yield);
			SELECT LAST_INSERT_ID();";
			recipe.Id = _db.ExecuteScalar<int>(sql, recipe);
			return recipe;
		}

		internal void Edit(EspressoRecipe original)
		{
			string sql = @"
			UPDATE espressorecipes
			SET
			title = @Title,
			grinder = @Grinder,
			grindSetting = @GrindSetting,
			description = @Description,
			machine = @Machine,
			Dose = @Dose,
			Temp = @Temp,
			PreinfusionTime = @PreinfusionTime,
			extractionTime = @ExtractionTime
			WHERE id = @Id";
			_db.Execute(sql, original);
		}

		internal void Delete(int id)
		{
			string sql = "DELETE FROM espressorecipes WHERE id = @id LIMIT 1";
			_db.Execute(sql, new { id });
		}
	}
}