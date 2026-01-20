using System.Linq.Expressions;
using Domain;
using NRules.RuleModel;
using NRules.RuleModel.Builders;

public class CustomRuleRepository : IRuleRepository
{
    private readonly IRuleSet _ruleSet = new RuleSet("MyRuleSet");

    public IEnumerable<IRuleSet> GetRuleSets()
    {
        return new[] {_ruleSet};
    }

    public void LoadRules()
    {
        //Assuming there is only one rule in this example
        var rule = BuildRule();
        _ruleSet.Add(new []{rule});
    }

    /**
    The example rule being built:
    
    When()
    .Match(() => customer, c => c.Name == "John Do")
    .Match(() => order, o => o.Customer == customer && o.Amount > 100.00m)
    .Then(ctx => Console.WriteLine("Customer {0} has an order in amount of ${1}", customer.Name, order.Amount));
    
    */
    private IRuleDefinition BuildRule()
    {
         //Create rule builder
    var builder = new RuleBuilder();
    builder.Name("TestRule");

    //Build conditions
    PatternBuilder customerPattern = builder.LeftHandSide().Pattern(typeof (Customer), "customer");
    Expression<Func<Customer, bool>> customerCondition = 
        customer => customer.Name == "John Do";
    customerPattern.Condition(customerCondition);

    PatternBuilder orderPattern = builder.LeftHandSide().Pattern(typeof (Order), "order");
    Expression<Func<Order, Customer, bool>> orderCondition1 = 
        (order, customer) => order.Customer == customer;
    Expression<Func<Order, bool>> orderCondition2 = 
        order => order.Amount > 100.00m;
    orderPattern.Condition(orderCondition1);
    orderPattern.Condition(orderCondition2);

    //Build actions
    Expression<Action<IContext, Customer, Order>> action = 
        (ctx, customer, order) => Console.WriteLine("Customer {0} has an order in amount of ${1}", customer.Name, order.Amount);
    builder.RightHandSide().Action(action);

    //Build rule model
    return builder.Build();
    }
}