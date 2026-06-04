# InventorySystem Tests

This directory contains unit and integration tests for the InventorySystem application.

## Test Structure

```
tests/InventorySystem.Tests/
├── Unit/
│   ├── CategoryDtoTests.cs          # Category DTO structure tests
│   ├── ProductDtoTests.cs           # Product DTO structure tests
│   └── StockDtoTests.cs             # Stock DTO structure tests
├── Integration/
│   ├── CategoryApiScenarioTests.cs  # Category API workflow scenarios
│   ├── ProductApiScenarioTests.cs   # Product API workflow scenarios
│   └── InventoryStockScenarioTests.cs # Stock tracking scenarios
└── README.md
```

## Running Tests

### Run All Tests
```bash
dotnet test
```

### Run Specific Test Class
```bash
dotnet test --filter "ClassName=CategoryDtoTests"
```

### Run Tests with Verbose Output
```bash
dotnet test -v d
```

### Run Tests with Coverage
```bash
dotnet test /p:CollectCoverage=true /p:CoverageFormat=opencover
```

## Test Categories

### Unit Tests - DTO Structure Validation

#### **CategoryDtoTests** 
- Tests for CategoryAddRequest, CategoryUpdateRequest, and CategoryResponse
- Validates DTO property mappings
- Ensures JSON serialization attributes work correctly

#### **ProductDtoTests**
- Tests for ProductResponse, ProductWithCategoryResponse, and ProductMinimalResponse
- Validates product data structure and relationships
- Tests multiple product handling

#### **StockDtoTests**
- Tests for StockRequest, StockResponse, and StockWithProductMinimalResponse
- Validates stock type handling (StockIn/StockOut)
- Tests quantity tracking

### Integration Tests - Business Scenario Workflows

#### **CategoryApiScenarioTests**
- Complete category add and retrieve workflows
- Category update scenarios
- Multiple category management

#### **ProductApiScenarioTests**
- Product creation and retrieval workflows
- Product with category relationships
- Multi-category product scenarios
- Inventory value calculations

#### **InventoryStockScenarioTests**
- Stock in/out tracking for single products
- Multi-product stock tracking
- Stock history management
- Low stock detection
- Inventory summary calculations

## Test Tools & Frameworks

- **xUnit** - Testing framework
- **FluentAssertions** - Fluent assertion library for readable tests
- **Entity Framework Core InMemory** - In-memory database support

## Testing Best Practices

1. **AAA Pattern** - All tests follow Arrange-Act-Assert structure
2. **Clear Naming** - Test names describe what is being tested and expected outcome
3. **DTO Validation** - Tests verify DTO structure and data mapping
4. **Scenario Based** - Integration tests cover realistic workflows
5. **Calculations** - Tests verify business logic calculations

## Example Test Pattern

```csharp
[Fact]
public void CategoryWorkflow_AddAndRetrieve_ShouldSucceed()
{
    // Arrange
    var categoryRequest = new CategoryAddRequest { Name = "Electronics" };

    // Act
    var categoryResponse = new CategoryResponse { Id = 1, Name = categoryRequest.Name };

    // Assert
    categoryResponse.Name.Should().Be(categoryRequest.Name);
}
```

## Running Tests from Command Line

```bash
# All tests
dotnet test

# Specific test file
dotnet test --filter "CategoryDtoTests"

# With details
dotnet test -v detailed

# With code coverage
dotnet test /p:CollectCoverage=true
```

## CI/CD Integration

These tests are designed to run in CI/CD pipelines:
- Fast execution
- No external dependencies required
- Reliable and deterministic results
- Clear failure messages

## Contributing

When adding new features:
1. Create corresponding DTO tests for new data structures
2. Add integration tests for new workflows
3. Ensure all tests pass before committing
4. Maintain or improve code coverage
