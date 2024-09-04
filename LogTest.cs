using NUnit.Framework;
using org.SpocWeb.root.Logging.Tests;
using Shouldly;

namespace org.SpocWeb.root.Logging;

//[TestSubject(typeof(Log))]
public class LogTest
{
    private const string Expected = "Failed to post to ESB. \n MurexValue in ProductMaskfield";

    readonly ChangedVariables _changedVariables = new()
    {
        MurexValue = nameof(ChangedVariables.MurexValue),
        ProductMaskValue = nameof(ChangedVariables.ProductMaskValue),
        ProductMaskfield = nameof(ChangedVariables.ProductMaskfield),
    };

    [Test]
    public void TestParsePositional()
    {
        var log = Log.Parse($"Failed to post to ESB. \n {_changedVariables.MurexValue} in {_changedVariables.ProductMaskfield}");
        log.Values.Length.ShouldBe(2);
        log.ToString().ShouldBe(Expected);
    }

    [Test]
    public void TestParseNamed()
    {
        var log = Log.Parse(null, "Failed to post to ESB. \n {murexValue} in {productMaskField}", _changedVariables.MurexValue, _changedVariables.ProductMaskfield);
        log.Values.Length.ShouldBe(2);
        log.ToString().ShouldBe(Expected);
    }
}