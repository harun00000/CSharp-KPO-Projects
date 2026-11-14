using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// Domain
services.AddSingleton<Rubric>();
services.AddSingleton<EvaluationFactory>();
services.AddSingleton<IFinalScoreStrategy, WeightedFinalScoreStrategy>();
services.AddSingleton<FinalScoreService>();

// Infrastructure
services.AddSingleton<IStudentRepository, InMemoryStudentRepository>();
services.AddSingleton<IEvaluationRepository, InMemoryEvaluationRepository>();

// Application
services.AddSingleton<StudentService>();
services.AddSingleton<RubricService>();
services.AddSingleton<EvaluationService>();
services.AddSingleton<ResultService>();
services.AddSingleton<PosFacade>();

services.AddSingleton<IResultExporter, CsvResultExporter>();
services.AddSingleton<ConsoleApp>();

using var provider = services.BuildServiceProvider();
var app = provider.GetRequiredService<ConsoleApp>();
app.Run();
