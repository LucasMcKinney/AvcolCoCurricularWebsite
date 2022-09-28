namespace AvcolCoCurricularWebsite.Data;

public static class DbInitializer
{
    public static void Initialize(AvcolCoCurricularWebsiteContext context)
    {
        context.Database.EnsureCreated();

        if (context.Staff.Any()) // Look for any staff
        {
            return; // DB has been seeded
        }

        var staff = new Staff[]
        {
            new Staff { FirstName = "Lindy", LastName = "Watkinson", TeacherCode = "LWA", HireDate = DateTime.Parse("2019-12-01") },
            new Staff { FirstName = "Jamie", LastName = "Smith", TeacherCode = "JSM", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Grace", LastName = "Williams", TeacherCode = "GWI", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Yasmin", LastName = "Khan", TeacherCode = "YKH", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Emma", LastName = "Watt", TeacherCode = "EWA", HireDate = DateTime.Parse("2017-10-01") },
            new Staff { FirstName = "Jaqui", LastName = "Brikhead", TeacherCode = "JBR", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Charlotte", LastName = "Houghton", TeacherCode = "CHO", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Andrew", LastName = "Bramston", TeacherCode = "ABR", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Santana ", LastName = "Schmidt", TeacherCode = "SSC", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Erica", LastName = "Norton", TeacherCode = "ENO", HireDate = DateTime.Parse("2014-09-01") },
            new Staff { FirstName = "Selina", LastName = "Tonkin", TeacherCode = "STO", HireDate = DateTime.Parse("2012-09-01") },
            new Staff { FirstName = "Sharon", LastName = "Cope", TeacherCode = "SCO", HireDate = DateTime.Parse("2014-09-09") },
            new Staff { FirstName = "Pushpa", LastName = "Dayal", TeacherCode = "PDA", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Jin", LastName = "Tai", TeacherCode = "JTA", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Hannah", LastName = "Normington", TeacherCode = "HNO", HireDate = DateTime.Parse("2013-09-06") },
            new Staff { FirstName = "James", LastName = "Went", TeacherCode = "JWE", HireDate = DateTime.Parse("2015-09-01") },
            new Staff { FirstName = "Chris", LastName = "Klaassen", TeacherCode = "CKL", HireDate = DateTime.Parse("2019-12-01") },
            new Staff { FirstName = "Anthony", LastName = "Munro", TeacherCode = "AMU", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "James", LastName = "Palmer", TeacherCode = "JPA", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Jean", LastName = "Mansill", TeacherCode = "JMA", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Grant", LastName = "Burden", TeacherCode = "GBU", HireDate = DateTime.Parse("2017-10-01") },
            new Staff { FirstName = "Elizabeth", LastName = "Atkinson", TeacherCode = "EAT", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Ngarama", LastName = "Howell", TeacherCode = "NHO", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Zameer", LastName = "Mazid", TeacherCode = "ZMA", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Julie", LastName = "Garner", TeacherCode = "JGA", HireDate = DateTime.Parse("2014-09-01") },
            new Staff { FirstName = "Jonelle", LastName = "Henry", TeacherCode = "JHE", HireDate = DateTime.Parse("2014-09-09") },
            new Staff { FirstName = "Brian", LastName = "Quedley", TeacherCode = "BQU", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "Philip", LastName = "Irwin", TeacherCode = "PIR", HireDate = DateTime.Parse("2017-09-01") },
            new Staff { FirstName = "G", LastName = "Hanna", TeacherCode = "GHA", HireDate = DateTime.Parse("2013-09-06") },
            new Staff { FirstName = "Jenifer", LastName = "Brown", TeacherCode = "JBO", HireDate = DateTime.Parse("2017-07-01") },
            new Staff { FirstName = "Bevan", LastName = "Jones", TeacherCode = "BJO", HireDate = DateTime.Parse("2021-01-01") },
            new Staff { FirstName = "Meg", LastName = "Goldthrope", TeacherCode = "MGO", HireDate = DateTime.Parse("2013-09-06") },
            new Staff { FirstName = "Neil", LastName = "Bartlett", TeacherCode = "NBA", HireDate = DateTime.Parse("2017-07-01") },
            new Staff { FirstName = "Shane", LastName = "Laurence", TeacherCode = "SLA", HireDate = DateTime.Parse("2020-09-01") }
        };

        context.Staff.AddRange(staff);
        context.SaveChanges();
    }
}