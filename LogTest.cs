using NUnit.Framework;
using org.SpocWeb.root.logging.Tests;
using Shouldly;
using org.SpocWeb.root.Attributes;

namespace org.SpocWeb.root.logging;


//[TestSubject(typeof(Log))]
/// <summary> NUnit tests verifying that <see cref="Log.Parse"/> correctly handles<br/>
/// both positional (string-interpolation) and named structured-log patterns. </summary>
/// <remarks>
/// ## Meta
/// pass: 2
/// mtime: 2026-05-15T20:55:32Z
/// digest: 9868bef24bec4ba8cbc06882f3c61128e618d418dfac4091cc7236c2ded6fa11
/// updated: 2026-05-19
/// </remarks>
[DocState(Pass = 2, MTime = "2026-08-03T19:25:31Z", Digest = "94888f5380c5428a3a4d5addb7594ff85b12a72aa94825922d7085d970429207", Stale = false, Path = "LogTest.cs", Since = "2026-08-18")]
public static class LogTest
{
    /// <summary>Specifies the constant expected.</summary>
    private const string Expected = "Failed to post to ESB. \n MoneyValue in ProductMaskField";

    /// <summary>Gets the _changed Variables.</summary>
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
		log.Values.ShouldBe([_changedVariables.MoneyValue, _changedVariables.ProductMaskField]);
		var formatted = log.ToString();
        formatted.ShouldBe(Expected);
        var pairs = log.ToDictionary();
        var keys = pairs.Keys.ToArray();
        keys.ShouldBe(["_changedVariables.MoneyValue", "_changedVariables.ProductMaskField"]);
		//keys.ShouldBe([ "0", "1" ]);
    }

    /// <summary> Verifies that <see cref="Log.Parse"/> extracts named placeholder keys from a<br/>
    /// conventional message-template and returns matching values and dictionary keys. </summary>
    [Test]
    public static void TestParseNamed()
    {
        var log = Log.Parse("Failed to post to ESB. \n {moneyValue} in {productMaskField}", _changedVariables.MoneyValue, _changedVariables.ProductMaskField);
        log.Values.Length.ShouldBe(2);
		log.Values.ShouldBe([_changedVariables.MoneyValue, _changedVariables.ProductMaskField]);
		var formatted = log.ToString();
        formatted.ShouldBe(Expected);
        var keys = log.ToDictionary().Keys.ToArray();
        keys.ShouldBe([ "moneyValue", "productMaskField" ]);
    }
}
