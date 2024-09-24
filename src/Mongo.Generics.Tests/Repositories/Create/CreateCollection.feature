Feature: Create a Collection - Repository

Scenario: Can InsertOneAsync
    Given a GenericRepository of PersonEntity
    And 1 PersonEntity
    When calling the InsertOneAsync method of the repository collection
    Then the collection of persons is created
    And the count of the collection of persons is equal to 1

Scenario Outline: Can InsertManyAsync
    Given a GenericRepository of PersonEntity
    And a list of <number> PersonEntities
    When calling the InsertManyAsync method of the repository collection
    Then the collection of persons is created
    And the count of the collection of persons is equal to <number>

    Examples:
        | number |
        | 10     |
        | 100    |
