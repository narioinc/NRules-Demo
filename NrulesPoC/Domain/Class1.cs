using System.Reflection;

namespace Domain;

public class WarrantyClaim(string name, string assetType, string region, float partsTotalCost)
{
   public string ClaimName { get;  set;} = name;
   public string AssetType { get;  set;} = assetType;
   public string Region { get;  set;} = region;

   public float PartsTotalCost { get; set;} = partsTotalCost;

   public Boolean IsApproved { get; set;} = false;

   public void ApproveClaim(Boolean b) => IsApproved = b;

    public override string ToString()
    {
        return $"{ClaimName}  {AssetType}";
    }
    
}


public class Customer
{
    public Customer(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
}

public class Order
{
    public Order(Customer customer, decimal amount)
    {
        Customer = customer;
        Amount = amount;
    }

    public Customer Customer { get; private set; }
    public decimal Amount { get; private set; }
}