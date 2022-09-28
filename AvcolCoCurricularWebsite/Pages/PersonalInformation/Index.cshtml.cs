namespace AvcolCoCurricularWebsite.Pages.PersonalInformation
{
    public class IndexModel : PageModel
    {
        private readonly AvcolCoCurricularWebsiteContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(AvcolCoCurricularWebsiteContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string StaffSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Models.PersonalInformation> PersonalInformation { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            StaffSort = sortOrder == "Staff" ? "staff_desc" : "Staff";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Models.PersonalInformation> personalinformationIQ = from p in _context.PersonalInformation
                                                                           select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                personalinformationIQ = personalinformationIQ.Where(s => s.Staff.LastName.ToUpper().Contains(searchString.ToUpper()));
            }

            personalinformationIQ = sortOrder switch
            {
                "staff_desc" => personalinformationIQ.OrderByDescending(s => s.Staff.LastName),
                _ => personalinformationIQ.OrderBy(s => s.Staff.LastName),
            };

            var pageSize = Configuration.GetValue("PageSize", 10);
            PersonalInformation = await PaginatedList<Models.PersonalInformation>.CreateAsync(personalinformationIQ.Include(p => p.Staff).AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}