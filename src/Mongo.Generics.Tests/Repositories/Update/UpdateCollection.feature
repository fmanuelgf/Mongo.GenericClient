Feature: Update a Collection - Repository

Scenario Outline: Can ReplaceOneAsync
    Given a GenericRepository of PersonEntity
    And a collection of 1 persons exist
    And the PersonEntity.<field> is modified to <value>
    When calling the ReplaceOneAsync method of the repository collection
    Then the person <field> in the collection equals <value>

    Examples:
        | field | value |
        | Name  | Tom   |
        | Age   | 30    |
