Feature: Read a Collection - Service

Scenario Outline: Can GetAll
    Given a ReadService of PersonEntity
    And a collection of 20 persons exist
    And 5 of the persons have the Name 'John'
    When calling the GetAll method of the ReadService with <filter> filter
    Then <expectedTotal> PersonEntities are returned
    And <expectedJohns> of the PersonEntities have the Name 'John'

    Examples:
        | filter       | expectedTotal | expectedJohns |
        | null         | 20            | 5             |
        | name is John | 5             | 5             |

Scenario Outline: Can GetPaginatedAsync
    Given a ReadService of PersonEntity
    And a collection of <number> persons exist
    And 10 of the persons have the Name 'John'
    When calling the GetPaginatedAsync(pageNumer: <page>, pageSize: <pageSize>) method of the ReadService with <filter> filter
    Then a PaginationResult of PersonEntities is returned
    And the PaginationResult.Page equals <page>
    And the PaginationResult.PageSize equals <pageSize>
    And the PaginationResult.TotalItems equals <expectedTotal>
    And the PaginationResult.Result.Count equals <pageItems>

    Examples:
        | number | filter       | page | pageSize |  expectedTotal | pageItems |
        | 10     | null         | 1    | 5        |  10            | 5         |
        | 50     | null         | 2    | 8        |  50            | 8         |
        | 100    | null         | 4    | 10       |  100           | 10        |
        | 10     | name is John | 1    | 5        |  10            | 5         |
        | 50     | name is John | 2    | 8        |  10            | 2         |
        | 100    | name is John | 4    | 10       |  10            | 0         |

Scenario: Can get the documents count using AsQueryable
    Given a ReadService of PersonEntity
    And a collection of 50 persons exist
    When calling the AsQueryable method of the ReadService
    Then Count equals 50

Scenario Outline: Can GetById
    Given a ReadService of PersonEntity
    And a collection of 50 persons exist
    And 1 of the persons have the Name 'John' and Id xyz
    When calling the GetByIdAsync xyz as <argType> method of the ReadService
    Then 1 PersonEntity is returned
    And the PersonEntity has the Name 'John'

    Examples:
        | argType  | 
        | ObjectId | 
        | string   | 

Scenario Outline: Cannot GetById - Invalid Id or Name
    Given a ReadService of PersonEntity
    And a collection of 50 persons exist
    When calling the GetByIdAsync foo method of the ReadService
    Then an Exception is thrown
    And the Exception message is 'foo is not a valid ObjectId'
