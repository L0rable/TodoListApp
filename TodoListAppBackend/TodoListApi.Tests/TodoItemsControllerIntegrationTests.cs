using Microsoft.AspNetCore.Mvc.Testing;
using TodoItemApi;

public class TodoItemsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public TodoItemsControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetTodoItems_ReturnsSuccessStatusCode()
    {
        // Act
        var response = await _client.GetAsync("/todoItems");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Contains("Task 1", responseString);
        Assert.Contains("Task 2", responseString);
        Assert.Contains("Task 3", responseString);
    }
}