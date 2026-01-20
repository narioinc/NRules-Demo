// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices.Marshalling;
using Domain;
using NRules;
using NRules.Fluent;
using Rules;

Console.WriteLine("Hello, World!");

var repository = new RuleRepository();
repository.Load(x => x.From(typeof(RegionRule).Assembly));




//Compile rules
var factory = repository.Compile();
var session = factory.CreateSession();


//event fires if a rule is matched and fired
session.Events.RuleFiredEvent += (_, args) 
    => {
        Console.WriteLine($"Fired rule: {args.Rule.Name}"); 
        var facts = args.Facts;
        foreach (var fact in facts){
           // Try to get the underlying fact object (IFactMatch implementations expose the matched value)
           var factObject = fact.GetType().GetProperty("Value")?.GetValue(fact)
                            ?? fact.GetType().GetProperty("Fact")?.GetValue(fact)
                            ?? fact.GetType().GetProperty("Object")?.GetValue(fact)
                            ?? (object)fact;
           Console.WriteLine($"Matched fact: {factObject}");
        }
    };

var claims = new[]
{
    new WarrantyClaim("w-abc","assetxyz", "CA", 561.09f),
    new WarrantyClaim("w-abc2","assetxyz3", "US", 349.09f),
    new WarrantyClaim("w-abc3","assetabc", "IN", 349.09f),
};

session.InsertAll(claims);

session.Fire();


