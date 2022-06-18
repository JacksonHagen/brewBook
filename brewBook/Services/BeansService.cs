using System;
using System.Collections.Generic;
using brewBook.Models;
using brewBook.Repositories;

namespace brewBook.Services
{
	public class BeansService
	{
		private readonly BeansRepository _repo;

		public BeansService(BeansRepository repo)
		{
			_repo = repo;
		}

		internal List<Bean> Get()
		{
			return _repo.Get();
		}

		internal Bean Get(int id)
		{
			Bean found = _repo.Get(id);
			if (found == null)
			{
				throw new Exception("Invalid Bean Id");
			}
			return found;
		}

		internal Bean Create(Bean bean)
		{
			return _repo.Create(bean);
		}
		internal void Delete(int id, string userId)
		{
			Bean target = Get(id);
			IsOwner(target.CreatorId, userId);
			_repo.Delete(id);
		}
		private void IsOwner(string id1, string id2) {
			if(id1 != id2)
			{
				throw new Exception("You are not the creator");
			}
		}
	}
}