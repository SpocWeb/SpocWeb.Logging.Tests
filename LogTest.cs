using NUnit.Framework;
using org.SpocWeb.root.Logging.Tests;
using Shouldly;

namespace org.SpocWeb.root.Logging;

//[TestSubject(typeof(Log))]
public static class LogTest
{
    private const string Expected = "Failed to post to ESB. \n MoneyValue in ProductMaskField";

    static readonly ChangedVariables _changedVariables = new()
    {
        MoneyValue = nameof(ChangedVariables.MoneyValue),
        ProductMaskValue = nameof(ChangedVariables.ProductMaskValue),
        ProductMaskField = nameof(ChangedVariables.ProductMaskField),
    };

	/// <summary> Tests parsing String Interpolation Log Statements </summary>
    [Test]
    public static void TestParsePositional()
    {
        var log = Log.Parse($"Failed to post to ESB. \n {_changedVariables.MoneyValue} in {_changedVariables.ProductMaskField}");
        log.Values.Length.ShouldBe(2);
        var formatted = log.ToString();
        formatted.ShouldBe(Expected);
        var pairs = log.ToDictionary();
        var keys = pairs.Keys.ToArray();
        keys.ShouldBe(["_changedVariables.MoneyValue", "_changedVariables.ProductMaskField"]);
		//keys.ShouldBe([ "0", "1" ]);
    }

    [Test]
    public static void TestParseNamed()
    {
        var log = Log.Parse(null, "Failed to post to ESB. \n {MoneyValue} in {productMaskField}", _changedVariables.MoneyValue, _changedVariables.ProductMaskField);
        log.Values.Length.ShouldBe(2);
        var formatted = log.ToString();
        formatted.ShouldBe(Expected);
        var keys = log.ToDictionary().Keys.ToArray();
        keys.ShouldBe([ "MoneyValue", "productMaskField" ]);
    }
}