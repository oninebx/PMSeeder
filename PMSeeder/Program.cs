// See https://aka.ms/new-console-template for more information
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMSeeder.Core;
using PMSeeder.Domain;

var builder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false);
IConfiguration config = builder.Build();
var generatorConfiguration = config.GetSection("GeneratorConfiguration").Get<GeneratorConfiguration>();
var services = new ServiceCollection()
    .AddSingleton<IGeneratorConfiguration>(generatorConfiguration!)
    .AddSingleton<IGeneratorFactory<string>, StringGeneratorFactory>()
    .AddSingleton<IDomainGeneratorFactory, DomainGeneratorFactory>()
    .BuildServiceProvider();

Console.WriteLine("Welcome to HealthSeeder!");
var domainGeneratorFactory = services.GetRequiredService<IDomainGeneratorFactory>();
var patientGenerator = domainGeneratorFactory.CreatePatientGenerator();
while ("P".Equals(Console.ReadLine()))
{
    var patient = patientGenerator.Generate();
    Console.WriteLine(patient.NHI);
}



//var patientGenerator = new PatientGenerator(new StringGeneratorFactory());
//var patient = patientGenerator.Generate();
//Console.WriteLine(patient.NHI);
