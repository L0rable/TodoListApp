using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
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

    [Fact]
    public async Task PostTodoItem_ReturnsCreatedStatusCode()
    {
        // Arrange
        var newItem = new TodoItem { Name = "New Task" };
        var json = JsonConvert.SerializeObject(newItem);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/todoItems", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var createdItem = JsonConvert.DeserializeObject<TodoItem>(responseString);
        Assert.Equal("New Task", createdItem.Name);
    }

    [Fact]
    public async Task DeleteTodoItem_ReturnsSuccessStatusCode()
    {
        // Arrange
        var itemId = 1;

        // Act
        var response = await _client.DeleteAsync("todoItems/" + itemId);

        // Assert
        response.EnsureSuccessStatusCode();
    }
}