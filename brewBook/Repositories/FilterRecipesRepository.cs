using System.Collections.Generic;
using System.Data;
using System.Linq;
using brewBook.Models;
using Dapper;

namespace brewBook.Repositories
{
	public class FilterRecipesRepository
	{
		private readonly IDbConnection _db;

		public FilterRecipesRepository(IDbConnection db)
		{
			_db = db;
		}

		internal List<FilterRecipe> Get()
		{
			string sql = @"
			SELECT
				fr.*,
				b.*,
				a.*
			FROM filterrecipes fr
			JOIN beans b on fr.beanId = b.id
			JOIN accounts a on fr.creatorId = a.id
			";
			return _db.Query<FilterRecipe, Bean, Account, FilterRecipe>(sql, (fr, b, a) =>
			{
				fr.Beans = b;
				fr.Creator = a;
				return fr;
			}).ToList();
		}

		internal FilterRecipe Get(int id)
		{
			string sql = @"
			SELECT
				fr.*,
				b.*,
				a.*
			FROM filterrecipes fr
			JOIN beans b ON fr.beanId = b.id
			JOIN accounts a ON fr.creatorId = a.id
			WHERE fr.id = @id;
			";
			return _db.Query<FilterRecipe, Bean, Profile, FilterRecipe>(sql, (fr, b, p) =>
			{
				fr.Beans = b;
				fr.Creator = p;
				return fr;
			}, new { id }).FirstOrDefault();
		}

		internal FilterRecipe Create(FilterRecipe recipe)
		{
			string sql = @"
			INSERT INTO filterrecipes
			(beanId, title, dose, temp, grinder, grindSetting, description, creatorId, timeInSeconds, totalWeight, brewer)
			VALUES
			(@BeanId, @Title, @Dose, @Temp, @Grinder, @GrindSetting, @Description, @CreatorId, @TimeInSeconds, @TotalWeight, @Brewer);
			SELECT LAST_INSERT_ID();
			";
			recipe.Id = _db.ExecuteScalar<int>(sql, recipe);
			return recipe;
		}

		internal void Edit(FilterRecipe original)
		{
			string sql = @"
			UPDATE filterrecipes
			SET
				beanId = @BeanId,
				title = @Title,
				dose = @Dose,
				temp = @Temp,
				grinder = @Grinder,
				grindSetting = @GrindSetting,
				description = @Description,
				timeInSeconds = @TimeInSeconds,
				totalWeight = @TotalWeight,
				brewer = @Brewer
			WHERE id = @Id;";
			_db.Execute(sql, original);
		}

		internal void Delete(int id)
		{
			string sql = "DELETE FROM filterrecipes WHERE id = @id LIMIT 1;";
			_db.Execute(sql, new { id });
		}
	}
}