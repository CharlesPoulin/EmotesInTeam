#Feature: Login
#  In order to access my account
#  As a user
#  I want to log in with my credentials
#
#Scenario: Successful login with valid credentials
#  Given I am on the login page
#  When I enter a valid username and password
#  Then I should be logged in successfully
#
#Scenario: Unsuccessful login with invalid credentials
#  Given I am on the login page
#  When I enter an invalid username and password
#  Then I should receive a login error
#
#
#
#[Binding]
#public class LoginSteps
#{
#    private readonly TestContext _context; // You need to set up a TestContext to handle requests
#
#    public LoginSteps(TestContext context)
#    {
#        _context = context;
#    }
#
#    [Given(@"I am on the login page")]
#    public void GivenIAmOnTheLoginPage()
#    {
#        // Navigate to the login page or set the current context to the login functionality
#    }
#
#    [When(@"I enter a valid username and password")]
#    public void WhenIEnterAValidUsernameAndPassword()
#    {
#        // Simulate entering valid credentials
#        _context.EnterCredentials("validUser", "validPassword");
#    }
#
#    [When(@"I enter an invalid username and password")]
#    public void WhenIEnterAnInvalidUsernameAndPassword()
#    {
#        // Simulate entering invalid credentials
#        _context.EnterCredentials("invalidUser", "invalidPassword");
#    }
#
#    [Then(@"I should be logged in successfully")]
#    public void ThenIShouldBeLoggedInSuccessfully()
#    {
#        // Assert the login was successful (e.g., check for a successful response or token)
#    }
#
#    [Then(@"I should receive a login error")]
#    public void ThenIShouldReceiveALoginError()
#    {
#        // Assert that a login error was received
#    }
#}
#
#public class TestContext
#{
#    private readonly HttpClient _client;
#
#    public TestContext()
#    {
#        var server = new TestServer(new WebHostBuilder()
#            .UseStartup<Startup>()); // Use your Startup class
#        _client = server.CreateClient();
#    }
#
#    public void EnterCredentials(string username, string password)
#    {
#        // Code to send a login request to your API
#    }
#
#    // Additional methods to handle responses, etc.
#}
