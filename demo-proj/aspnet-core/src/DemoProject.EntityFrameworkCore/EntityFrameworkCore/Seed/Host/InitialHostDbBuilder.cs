namespace DemoProject.EntityFrameworkCore.Seed.Host;

public class InitialHostDbBuilder
{
    private readonly DemoProjectDbContext _context;

    public InitialHostDbBuilder(DemoProjectDbContext context)
    {
        _context = context;
    }

    public void Create()
    {
        new DefaultEditionCreator(_context).Create();
        new DefaultLanguagesCreator(_context).Create();
        new HostRoleAndUserCreator(_context).Create();
        new DefaultSettingsCreator(_context).Create();
        
        new InitialBookCategoryCreator(_context).Create();

        _context.SaveChanges();
    }
}
