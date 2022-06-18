using System.Collections.Generic;
using System.Data;
using System.Linq;
using brewBook.Models;
using Dapper;

namespace brewBook.Repositories
{
	public class BeansRepository
	{
		private readonly IDbConnection _db;

		public BeansRepository(IDbConnection db)
		{
			_db = db;
		}

		internal List<Bean> Get()
		{
			string sql = @"
			SELECT
				b.*,
				a.*
			FROM beans b
			JOIN accounts a ON a.id = b.creatorId
			";
			return _db.Query<Bean, Profile, Bean>(sql, (b, p) =>
			{
				b.CreatorId = p.Id;
				return b;
			}).ToList();
		}

		internal Bean Get(int id)
		{
			string sql = @"
			SELECT
				b.*,
				a.*
			FROM beans b
			JOIN accounts a ON a.id = b.creatorId
			WHERE b.id = @Id;
			";
			return _db.Query<Bean, Profile, Bean>(sql, (b, p) =>
			{
				b.CreatorId = p.Id;
				return b;
			}, new { id }).FirstOrDefault();
		}

		internal Bean Create(Bean bean)
		{
			string sql = @"
			INSERT INTO beans
			(origin, roaster, process, roastLevel, creatorId)
			VALUES
			(@Origin, @Roaster, @Process, @RoastLevel, @CreatorId);
			SELECT LAST_INSERT_ID();
			";
			bean.Id = _db.ExecuteScalar<int>(sql, bean);
			return bean;
		}

		internal void Delete(int id)
		{
			string sql = "DELETE FROM beans WHERE id = @id LIMIT 1";
			_db.Execute(sql, new { id });
		}
	}
}