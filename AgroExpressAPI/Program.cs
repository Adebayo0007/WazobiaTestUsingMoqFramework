using AgroExpressAPI.ProgramHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ProgrameHelperClass.CrossOriginPolicy(builder);//Cross origin Policy
ProgrameHelperClass.AdminPolicy(builder);//Adding a policy to an End-point or Controller
ProgrameHelperClass.AddingContextAccessor(builder);//Adding contect accessor to the containe
ProgrameHelperClass.RegisteringAndSortingDependencies(builder);//Registering,sorting and determining the life cycle of dependencies
ProgrameHelperClass.AddingDbContextToContainer(builder);// Adding BdContext class to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
   c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Wazobia Agro Express",Version = "v1"});
});
ProgrameHelperClass.AddingJWTConfigurationToContainer(builder);//Adding JWT Configuration to the container

var app = builder.Build();

// Configure the HTTP request pipeline.
ProgrameHelperClass.HttpPipelineConfiguration(app);

app.Run();
