Feature: Create a Collection

Scenario: Can InsertOneAsync
    Given a GenericRepository of PersonEntity
    And 1 PersonEntity
    When calling the InsertOneAsync method of the repository collection
    Then the collection of Persons is created
    And the collection count equals 1

Scenario Outline: Can InsertManyAsync
    Given a GenericRepository of PersonEntity
    And a list of <number> PersonEntities
    When calling the InsertManyAsync method of the repository collection
    Then the collection of Persons is created
    And the collection count equals <number>

    Examples:
        | number |
        | 10     |
        | 100    |
