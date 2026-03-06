namespace org.SpocWeb.root.logging.Tests;

using NUnit.Framework;
using Serilog;
using Serilog.Sinks.TestCorrelator;
using Shouldly;
using Microsoft.Extensions.Logging;

[TestFixture]
public class LoggingTests {

	private ILogger<LoggingTests> _logger;

	[OneTimeSetUp]
	public void GlobalSetup() {
		// Configure the logger once for the entire test fixture
		// Bridge Serilog to Microsoft ILogger for the test
		var serilogLogger = new LoggerConfiguration()
			.WriteTo.TestCorrelator()
			.CreateLogger();

		var loggerFactory = new LoggerFactory()
			.AddSerilog(serilogLogger);

		_logger = loggerFactory.CreateLogger<LoggingTests>();
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

		// Act
		_logger.LogEvent(prefix, $"Action {action} attempted by {userId}");

		// Assert
		var logEvents = TestCorrelator.GetLogEventsFromCurrentContext().ToList();

		logEvents.Count.ShouldBe(1);
		var logEvent = logEvents.First();

		// Shouldly assertions for structured properties
		logEvent.Properties["Security_action"].ToString().ShouldBe('"' + action + '"');

		logEvent.Properties["Security_userId"].ToString().ShouldBe('"' + userId + '"');

		// Verify the Ambient Context
		logEvent.Properties["context"].ToString().ShouldBe('"' + prefix + '"');
	}

	[Test]
	public void LogEvent_Should_Capture_Variable_Names_Without_Prefix() {
		using var context = TestCorrelator.CreateContext();

		// Act
		_logger.LogEvent($"Action {action} attempted by {userId}");

		// Assert
		var logEvents = TestCorrelator.GetLogEventsFromCurrentContext().ToList();

		logEvents.Count.ShouldBe(1);
		var logEvent = logEvents.First();

		logEvent.Properties[nameof(action)].ToString().ShouldBe('"' + action + '"');

		logEvent.Properties[nameof(userId)].ToString().ShouldBe('"' + userId + '"');
	}

	[Test]
	public void LogEvent_Should_Destructure_Objects_When_Requested() {
		// Arrange
		using var context = TestCorrelator.CreateContext();
		var payload = new { Temp = 22.5, Status = "OK" };

		// Act
		_logger.LogEvent("Sensor", $"Readout: {payload.Destructure()}");

		// Assert
		var logEvents = TestCorrelator.GetLogEventsFromContextId(context.Id);
		var logEvent = logEvents.Single();

		// Verify the property exists with the destructuring prefix (@) in the template logic

		// When destructured, the ToString() usually shows the internal structure
		var propertyValue = logEvent.Properties["Sensor_payload"].ToString();
		propertyValue.ShouldContain("Temp: 22.5");
		propertyValue.ShouldContain("Status: \"OK\"");
	}
}