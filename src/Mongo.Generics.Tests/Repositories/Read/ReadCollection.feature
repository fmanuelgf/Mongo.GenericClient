Feature: Read a Collection

Scenario Outline: Can InsertManyAsync
    Given a GenericRepository of PersonEntity
    And a collection of <number> PersonEntities exist(s)
    When calling the Find method of the repository collection
    Then a list of <number> PersonEntities is returned

    Examples:
        | number |
        | 1      |
        | 10     |
        | 100    |
