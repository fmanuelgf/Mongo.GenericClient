Feature: Read a Collection

Scenario Outline: Can GetPaginatedAsync
    Given a ReadService of PersonEntity
    And a collection of <number> PersonEntities exist(s)
    When calling the GetPaginatedAsync(pageNumer: <page>, pageSize: <pageSize>) method of the ReadService
    Then a PaginationResult of PersonEntities is returned
    And the PaginationResult.Page is <page>
    And the PaginationResult.PageSize is <pageSize>
    And the PaginationResult.TotalItems is <number>
    And the PaginationResult.Result.Count is <pageItems>

    Examples:
        | number | page | pageSize | pageItems |
        | 1      | 1    | 5        | 1         |
        | 10     | 2    | 4        | 4         |
        | 100    | 4    | 10       | 10        |
