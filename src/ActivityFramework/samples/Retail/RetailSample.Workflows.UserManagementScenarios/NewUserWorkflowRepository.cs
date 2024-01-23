namespace RetailSample.Workflows.UserManagementScenarios;

public sealed class NewUserWorkflowRepository(IDbContextFactory<RetailSampleDbContext> dbContextFactory)
        : IWorkflowRepository<NewUserWorkflowParameters, NewUserWorkflowState>
{
    private readonly IDbContextFactory<RetailSampleDbContext> _dbContextFactory = dbContextFactory;

    public async Task<NewUserWorkflowState> Load(NewUserWorkflowParameters parameters)
    {
        var dbContext = _dbContextFactory.CreateDbContext();
        var user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return new NewUserWorkflowState()
        {
            UserId = parameters.UserId,
            User = user
        };
    }

    public async Task Persist(NewUserWorkflowState workflowState)
    {
        var dbContext = _dbContextFactory.CreateDbContext();


        await dbContext.SaveChangesAsync();
    }
}
