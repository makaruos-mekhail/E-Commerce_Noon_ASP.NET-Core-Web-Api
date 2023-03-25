using Context;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_API.Reposatories
{
    public class Reposatory<TE, Tid> where TE : class
    {
        protected readonly DContext context;
        private readonly DbSet<TE> dbset;
        public Reposatory(DContext _con)
        {
            dbset = _con.Set<TE>();
			context = _con;
        }
        public async Task<TE> CreateAsync(TE entity)
        {

            var data = (await dbset.AddAsync(entity)).Entity;
            await context.SaveChangesAsync();
            return data;
        }

        public async Task<TE?> GetByIDAsync(Tid id)
        {
            return await dbset.FindAsync(id);
        }

        //public async Task<TE?> GetAll()
        //{
        //    return await dbset.ToList();
        //}

        public async Task<bool> UpdateAsync(TE entity)
        {
            var en = dbset.Update(entity);
            if (en != null)
            {
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

		public async Task<bool> DeleteAsync(Tid id)
		{
			var i = await dbset.FindAsync(id);
			if (i != null)
			{
				dbset.Remove(i);
				await context.SaveChangesAsync();
				return true;
			}
			return false;
		}


	}
}
