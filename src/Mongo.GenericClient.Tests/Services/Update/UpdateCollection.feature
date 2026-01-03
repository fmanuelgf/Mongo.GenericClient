Feature: Update a Collection - Service

Scenario Outline: Can UpdateOneAsync
    Given a WriteService of PersonEntity
    And a collection of 1 persons exist
    And the PersonEntity.<field> is modified to <value>
    When calling the UpdateAsync method of the WriteService
    Then person.<field> in the collection equals <value>

    Examples:
        | field | value |
        | Name  | Frank |
        | Age   | 30    |
        | Name  | Tom   |
        | Age   | 40    |
