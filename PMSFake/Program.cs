// See https://aka.ms/new-console-template for more information
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMSFake.Core;
using PMSFake.Domain;

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

// var appointmentGenerator = domainGeneratorFactory.CreateAppointmentGenerator();
while (!string.IsNullOrEmpty(Console.ReadLine()))
{
    var input = Console.ReadLine()?.Trim();
    if ("P".Equals(input))
    {
        //var patient = patientGenerator.Generate();
        var patient = patientGenerator.Generate();
        Console.WriteLine(patient.NHI);
    }
    // if ("A".Equals(input))
    // {
    //     var appointment = appointmentGenerator.Generate();
    //     Console.WriteLine(appointment);
    // }
    
}



//var patientGenerator = new PatientGenerator(new StringGeneratorFactory());
//var patient = patientGenerator.Generate();
//Console.WriteLine(patient.NHI);
