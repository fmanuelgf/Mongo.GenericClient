Feature: Read a Collection - Service

Scenario Outline: Can GetPaginatedAsync
    Given a ReadService of PersonEntity
    And a collection of <number> persons exist
    When calling the GetPaginatedAsync(pageNumer: <page>, pageSize: <pageSize>) method of the ReadService
    Then a PaginationResult of PersonEntities is returned
    And the PaginationResult.Page equals <page>
    And the PaginationResult.PageSize equals <pageSize>
    And the PaginationResult.TotalItems equals <number>
    And the PaginationResult.Result.Count equals <pageItems>

    Examples:
        | number | page | pageSize | pageItems |
        | 1      | 1    | 5        | 1         |
        | 10     | 2    | 4        | 4         |
        | 100    | 4    | 10       | 10        |

Scenario: Can get the documents count using AsQueryable
    Given a ReadService of PersonEntity
    And a collection of 50 persons exist
    When calling the AsQueryable method of the ReadService
    Then Count equals 50