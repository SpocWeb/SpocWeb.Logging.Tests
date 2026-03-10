namespace org.SpocWeb.root.logging.Tests;

using NUnit.Framework;
using Serilog;
using Serilog.Sinks.TestCorrelator;
using Shouldly;
using Microsoft.Extensions.Logging;

[TestFixture]
public class SemanticLogTests {

	private ILogger<SemanticLogTests> _logger;

	[OneTimeSetUp]
	public void GlobalSetup() {
		// Configure the logger once for the entire test fixture
		// Bridge Serilog to Microsoft ILogger for the test
		var serilogLogger = new LoggerConfiguration()
			.WriteTo.TestCorrelator()
			.CreateLogger();

		var loggerFactory = new LoggerFactory().AddSerilog(serilogLogger);

		_logger = loggerFactory.CreateLogger<SemanticLogTests>();
		//_logger = new LoggerConfiguration()
		//	.WriteTo.TestCorrelator()
		//	.CreateLogger();
	}

	const string userId = "User_123";
	const string action = "DeleteAccount";
	const string prefix = "Security";

	[Test]
	public void LogEvent_Should_Capture_Variable_Names_With_Prefix() {
		// Arrange
		using ITestCorrelatorContext context = TestCorrelator.CreateContext();
		var exception = new Exception();

		// Act
		_logger.Logg(prefix, $"Action {action} attempted by {userId}", exception);

		// Assert
		var logEvents = TestCorrelator.GetLogEventsFromCurrentContext().ToList();

		logEvents.Count.ShouldBe(1);
		Serilog.Events.LogEvent logEvent = logEvents.First();

		logEvent.Exception.ShouldBeSameAs(exception);
		logEvent.Level.ShouldBe(Serilog.Events.LogEventLevel.Error);

		logEvent.Properties["Security_action"].ToString().ShouldBe('"' + action + '"');
		logEvent.Properties["Security_userId"].ToString().ShouldBe('"' + userId + '"');
		// Verify the Ambient Context
		logEvent.Properties["context"].ToString().ShouldBe('"' + prefix + '"');
		logEvent.MessageTemplate.Text.ShouldBe("Action {Security_action} attempted by {Security_userId}");
	}

	[Test]
	public void LogEvent_Should_Capture_Variable_Names_Without_Prefix() {
		using var context = TestCorrelator.CreateContext();
		var exception = new Exception();

		// Act
		_logger.Logg($"Action {action} attempted by {userId}", exception);

		// Assert
		var logEvents = TestCorrelator.GetLogEventsFromCurrentContext().ToList();

		logEvents.Count.ShouldBe(1);
		var logEvent = logEvents.First();

		logEvent.Properties[nameof(action)].ToString().ShouldBe('"' + action + '"');
		logEvent.Properties[nameof(userId)].ToString().ShouldBe('"' + userId + '"');

		logEvent.Exception.ShouldBeSameAs(exception);
		logEvent.Level.ShouldBe(Serilog.Events.LogEventLevel.Error);
	}

	[Test]
	public void LogEvent_Should_Destructure_Objects_When_Requested() {
		// Arrange
		using var context = TestCorrelator.CreateContext();
		var payload = new { Temp = 22.5, Status = "OK" };

		// Act
		_logger.Logg("Sensor", $"Readout: {payload.Destructure()}");

		// Assert
		var logEvents = TestCorrelator.GetLogEventsFromContextId(context.Id);
		var logEvent = logEvents.Single();

		// Verify the property exists with the destructuring prefix (@) in the template logic

		// When destructured, the ToString() usually shows the internal structure
		var payLoad = logEvent.Properties["Sensor_payload"]; //is a String when 
		var propertyValue = payLoad.ToString();
		propertyValue.ShouldContain("Temp: 22.5");
		propertyValue.ShouldContain("Status: \"OK\"");
	}
}