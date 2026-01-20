
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Domain;
using NRules.Fluent.Dsl;

namespace Rules;

public class RegionRule : Rule
{
    public override void Define()
    {
        WarrantyClaim claim = default!;
        When().Match(() => claim, c => c.Region == "US");
        
        Then().Do(ctx => claim.ApproveClaim(true));
    }
}

public class ComplexClaimRule : Rule
{
    public override void Define()
    {
        WarrantyClaim claim = default!;
        When()
        .Match(() => claim, c => c.PartsTotalCost > 500 && c.Region == "CA");

        Then().Do(ctx => claim.ApproveClaim(false));
    }
}

public class ComplexClaim2Rule : Rule
{
    public override void Define()
    {
        WarrantyClaim claim = default!;
        When()
        .Match(() => claim, c => c.Region == "IN" && c.PartsTotalCost > 100 && c.AssetType != "assetabc1");

        Then().Do(ctx => claim.ApproveClaim(false));
    }
}