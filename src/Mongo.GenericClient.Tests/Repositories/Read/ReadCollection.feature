Feature: Read a Collection - Repository

Scenario Outline: Can Find
    Given a GenericRepository of PersonEntity
    And a collection of <number> persons exist
    When calling the Find method of the repository collection
    Then a list of <number> PersonEntities is returned

    Examples:
        | number |
        | 1      |
        | 10     |
        | 100    |
