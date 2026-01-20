// See https://aka.ms/new-console-template for more information
using Domain;
using NRules;

Console.WriteLine("Hello, World!");
var repository = new CustomRuleRepository();
repository.LoadRules();
ISessionFactory factory = repository.Compile();

ISession session = factory.CreateSession();
var customer = new Customer("John Do");
session.Insert(customer);
session.Insert(new Order(customer, 90.00m));
session.Insert(new Order(customer, 110.00m));

session.Fire();
