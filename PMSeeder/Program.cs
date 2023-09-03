// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using PMSeeder.Core;
using PMSeeder.Domain;
using PMSeeder.Domain.Entities;

//HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
//builder.Services.AddSingleton<IGeneratorFactory<string>, StringGeneratorFactory>();
//using IHost host = builder.Build();

//await host.RunAsync();

var services = new ServiceCollection()
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
