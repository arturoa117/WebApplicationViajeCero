using WebApplicationViajeCero.Models;

namespace WebApplicationViajeCero.Utils
{
    public class Report
    {
        public static IQueryable<Request> FilterByDateRange(IQueryable<Request> requests, DateTime? from, DateTime? to)
        {
            if (from.HasValue && to.HasValue)
            {
                DateTime fromDate = from.Value.Date;
                DateTime toDate = to.Value.Date;

                requests = requests.Where(r =>
                    r.DateCreated.Date >= fromDate &&
                    r.DateModificated.Date <= toDate
                );
            }
            else if (from.HasValue)
            {
                DateTime fromDate = from.Value.Date;
                requests = requests.Where(r => r.DateCreated.Date >= fromDate);
            }
            else if (to.HasValue)
            {
                DateTime toDate = to.Value.Date;
                requests = requests.Where(r => r.DateModificated.Date <= toDate);
            }

            return requests;
        }

    }
}
